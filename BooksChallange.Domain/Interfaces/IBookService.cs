using BooksChallange.Domain.Models;
using System.Collections.Generic;

namespace BooksChallange.Domain.Interfaces
{
    public interface IBookService
    {
        Book GetById(int id);

        Book Create(string title = "", string description = "", string isbn = "", string language = "");

        IEnumerable<Book> List();
    }
}
