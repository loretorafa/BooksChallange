using BooksChallange.Domain.Entities;
using BooksChallange.Infrastructure.DataAccess.Context;

namespace BooksChallange.Infrastructure.DataAccess.Repositories
{
    public class BookRepository : BaseRepository<Book>
    {
        public BookRepository() : base(new BookChallangeContext())
        { }
    }
}
