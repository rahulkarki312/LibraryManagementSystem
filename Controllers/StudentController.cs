using LibraryManagement.Models;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Models.Entities;
using LibraryManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace LibraryManagementSystem.Controllers
{

    [Authorize(Roles = "student")]
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;
        public StudentController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var currentUser = dbContext.AspNetUsers.FirstOrDefault(u => u.UserName == User.Identity.Name);


            string firstName = currentUser.FirstName;
            string lastName = currentUser.LastName;
            

            var user = new UserViewModel {
                FirstName = firstName,
                LastName = lastName
            };

            return View(user);
        }


        [HttpGet]
        public async Task<IActionResult> List()
        {
            var books = await dbContext.Books.ToListAsync();
            return View(books);

        }

        [HttpGet]
        public async Task<IActionResult> Burrow(Guid bookId)
        {
            

            var book = await dbContext.Books.FindAsync(bookId);

            var newBurrow = new BurrowRecordViewModel
            {

                BookId = bookId.ToString(),
                StudentId = User.Identity.Name,
                BurrowDate = DateTime.Now,
                ReturnDate = DateTime.Now.AddDays(7),
                IsFined = false,
                BookTitle = book.Title,
                BookAuthor = book.Author,
                IsAvailable = book.IsAvailable,

            };
            //return View(book);
            return View(newBurrow);
        }

        [HttpPost]
        public async Task<ActionResult> Burrow(BurrowRecordViewModel viewModel)
        {

            var newBurrow = new BurrowRecord
            {
                BookId = viewModel.BookId,
                StudentId = viewModel.StudentId,
                BurrowDate = viewModel.BurrowDate,
                ReturnDate = viewModel.ReturnDate,
                IsFined = viewModel.IsFined
            };
            await dbContext.BurrowRecords.AddAsync(newBurrow);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("List", "Student");

        }

        [HttpGet]
        public async Task<IActionResult> ListBurrowRecords()
        {
            var currentUserStudentId = User.Identity.Name;

            // Fetch BurrowRecords for the current user from the database
            var burrowRecordsForCurrentUser = await dbContext.BurrowRecords
                .Where(br => br.StudentId == currentUserStudentId)
                .ToListAsync();

            // Convert BookId values to Guid and perform the join in memory
            var recordsWithBookInfo = burrowRecordsForCurrentUser
                .Join(
                    dbContext.Books,
                    br => Guid.Parse(br.BookId),
                    b => b.Id,
                    (br, b) => new BurrowRecordViewModel
                    {
                        Id = br.Id,
                        BookId = br.BookId,
                        StudentId = br.StudentId,
                        BurrowDate = br.BurrowDate,
                        ReturnDate = br.ReturnDate,
                        IsFined = br.IsFined,
                        BookTitle = b.Title,
                        BookAuthor = b.Author,
                        IsAvailable = b.IsAvailable
                    }
                )
                .ToList(); 

            return View(recordsWithBookInfo);
        }

        [HttpPost]
        public async Task<IActionResult> ReturnBook(Guid burrowRecordId)
        {
            var burrowRecord = await dbContext.BurrowRecords.FindAsync(burrowRecordId);
            if (burrowRecord != null)
            {
                dbContext.BurrowRecords.Remove(burrowRecord);
                await dbContext.SaveChangesAsync();

            }
            return RedirectToAction("ListBurrowRecords", "Student");
        }
    }

}

