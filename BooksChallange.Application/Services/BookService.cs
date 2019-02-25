using BooksChallange.Domain.Interfaces;
using BooksChallange.Domain.Entities;

namespace BooksChallange.Application.Services
{
    public class BookService : BaseService<Book>
    {
        public BookService(IRepository<Book> repository) : base(repository)
        {
        }
    }
}
