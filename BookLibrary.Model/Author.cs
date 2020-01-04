using System.Collections.Generic;

namespace BookLibrary.Model
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; } = new HashSet<Book>(); // this is a browsing property, not found in the database.
    }
}
