namespace BookLibrary.Model
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; } // this is a browsing property, not found in the database.
    }
}
