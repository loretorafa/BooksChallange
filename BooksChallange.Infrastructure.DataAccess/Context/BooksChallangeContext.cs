using BooksChallange.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BooksChallange.Infrastructure.DataAccess.Context
{
    public class BooksChallangeContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public BooksChallangeContext() : base() { }

        public BooksChallangeContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseInMemoryDatabase("BooksChallangeTests.db");
        }
    }
}
