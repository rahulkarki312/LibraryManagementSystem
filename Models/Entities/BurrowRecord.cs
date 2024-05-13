namespace LibraryManagementSystem.Models.Entities
{
    public class BurrowRecord
    {
        public Guid Id { get; set; }
        public string StudentId { get; set; }
        public string BookId { get; set; }
        public DateTime BurrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public bool IsFined { get; set; }

    }
}
