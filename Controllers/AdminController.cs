﻿using LibraryManagementSystem.Models;
using LibraryManagementSystem.Models.Entities;
using LibraryManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers
{
    [Authorize(Roles ="admin")]

    //all the actions for this controller are only limited to admin 


    public class AdminController : Controller
    {

        private readonly ApplicationDbContext dbContext;

        public AdminController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();

        }

        [HttpPost]
        public async Task<ActionResult> Add(AddBookViewModel viewModel)
        {

            var book = new Book
            {
                Title = viewModel.Title,
                Author = viewModel.Author,
                IsAvailable = viewModel.IsAvailable,
                PublishDate = viewModel.PublishDate,
            };
            await dbContext.Books.AddAsync(book);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("List", "Admin");
            //return View();
        }

        [HttpGet]

        public async Task<IActionResult> List()
        {
            var books = await dbContext.Books.ToListAsync();
            return View(books);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var book = await dbContext.Books.FindAsync(id);
            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Book viewModel)
        {
            var book = await dbContext.Books.FindAsync(viewModel.Id);
            if (book != null)
            {
                book.Title = viewModel.Title;
                book.Author = viewModel.Author;
                book.PublishDate = viewModel.PublishDate;
                book.IsAvailable = viewModel.IsAvailable;
                await dbContext.SaveChangesAsync();

            }
            return RedirectToAction("List", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Book viewModel)
        {
            var student = await dbContext.Books.FindAsync(viewModel.Id);
            //if finding and deleting the way used above throws error , use the below mothod to delete
            //   var student = await dbContext.Students.AsNoTracking().FirstOrDefaultAsync(x => x.Id ==viewModel.Id);
            if (student != null)
            {
                dbContext.Books.Remove(student);
                await dbContext.SaveChangesAsync();

            }
            return RedirectToAction("List", "Admin");
        }

    }
}
