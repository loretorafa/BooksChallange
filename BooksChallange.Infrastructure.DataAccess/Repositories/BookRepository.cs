using BooksChallange.Domain.Entities;
using BooksChallange.Domain.Interfaces.Repositories;
using BooksChallange.Infrastructure.DataAccess.Context;

namespace BooksChallange.Infrastructure.DataAccess.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(BooksChallangeContext context) : base(context) { }
    }
}
