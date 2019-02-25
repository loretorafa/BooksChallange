using BooksChallange.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BooksChallange.Infrastructure.DataAccess.Context
{
    public class BookChallangeContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseInMemoryDatabase("BookChallangeTest.db");
        }
    }
}
