using BooksChallange.Domain.Entities;
using System.Collections.Generic;

namespace BooksChallange.Domain.Interfaces.Repositories
{
    public interface IBookCrawler
    {
        List<Book> GetBooks();
    }
}
