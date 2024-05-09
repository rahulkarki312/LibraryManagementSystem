namespace LibraryManagementSystem.Models.Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime PublishDate { get; set; }

    }

}
