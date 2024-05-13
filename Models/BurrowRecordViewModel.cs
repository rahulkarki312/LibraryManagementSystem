using System.Security.Policy;

namespace LibraryManagementSystem.Models
{
    public class BurrowRecordViewModel
    {
        public Guid Id { get; set; }
        public string StudentId { get; set; }
        public string BookId { get; set; }
        public DateTime BurrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public bool IsFined { get; set; }
        public String BookTitle { get; set; }
        public String BookAuthor { get; set; }
        public bool IsAvailable { get; set; }
        public String StudentName { get; set;}

    }
}
