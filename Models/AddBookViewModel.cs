namespace LibraryManagementSystem.Models
{
    public class AddBookViewModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsAvailable { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
