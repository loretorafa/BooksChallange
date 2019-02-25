using BooksChallange.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BooksChallange.Api.Responses
{
    public class BooksResponse
    {
        public BooksResponse(IEnumerable<Book> books)
        {
            this.Books = books;
        }

        public int NumberBooks { get { return Books.Count(); } }

        public IEnumerable<Book> Books { get; set; }
    }
}
