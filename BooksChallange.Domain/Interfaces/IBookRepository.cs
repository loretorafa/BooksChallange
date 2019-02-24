using BooksChallange.Domain.Models;
using System.Collections.Generic;

namespace BooksChallange.Domain.Interfaces
{
    public interface IBookRepository
    {
        Book Get(int id);

        Book Create(Book book);

        IEnumerable<Book> List();
    }
}
