using BookLibrary.Model;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.DB
{
    public class BookContext:DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        public BookContext(DbContextOptions<BookContext> options)
            : base(options)
        {
        }

        public BookContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Library;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
