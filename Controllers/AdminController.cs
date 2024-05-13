using LibraryManagementSystem.Models;
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

       

        [HttpGet]
        public async Task<IActionResult> ListBurrowRecords()
        {
            // Retrieve all burrow records
            var allBurrowRecords = await dbContext.BurrowRecords.ToListAsync();

            // Join BurrowRecords with Books to get book information
            var recordsWithBookInfo = allBurrowRecords
                .Join(
                    dbContext.Books,
                    br => Guid.Parse(br.BookId),
                    b => b.Id,
                    (br, b) => new
                    {
                        br.Id,
                        br.BookId,
                        br.StudentId,
                        br.BurrowDate,
                        br.ReturnDate,
                        br.IsFined,
                        BookTitle = b.Title,
                        BookAuthor = b.Author,
                        b.IsAvailable
                    }
                )
                // Join with AspNetUsers to get user information
                .Join(
                    dbContext.AspNetUsers,
                    record => record.StudentId,
                    user => user.UserName,
                    (record, user) => new BurrowRecordViewModel
                    {
                        Id = record.Id,
                        BookId = record.BookId,
                        StudentId = record.StudentId,
                        BurrowDate = record.BurrowDate,
                        ReturnDate = record.ReturnDate,
                        IsFined = record.IsFined,
                        BookTitle = record.BookTitle,
                        BookAuthor = record.BookAuthor,
                        IsAvailable = record.IsAvailable,
                        StudentName = $"{user.FirstName} {user.LastName}" // Combine FirstName and LastName into UserName
                    }
                )
                .ToList();

            return View(recordsWithBookInfo);
        }


        [HttpPost]
        public async Task<IActionResult> FineStudent(Guid burrowRecordId)
        {
            var burrowRecord = await dbContext.BurrowRecords.FindAsync(burrowRecordId);
            if (burrowRecord != null)
            {
                burrowRecord.IsFined = true;

                await dbContext.SaveChangesAsync();

            }
            return RedirectToAction("ListBurrowRecords", "Admin");
        }
    }
}
