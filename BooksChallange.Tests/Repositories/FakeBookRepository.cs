using BooksChallange.Domain.Interfaces;
using BooksChallange.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace BooksChallange.Tests.Repositories
{
    public class FakeBookRepository : IRepository<Book>
    {
        private List<Book> _fakeDb;

        public FakeBookRepository(int itemCount = 0)
        {
            this.PopulateFakeRepository(itemCount);
        }

        public Book Select(int id)
        {
            return _fakeDb.SingleOrDefault(b => b.Id == id);
        }

        public Book Insert(Book book)
        {
            book.Id = _fakeDb.Count() + 1;

            _fakeDb.Add(book);

            return Select(book.Id);
        }

        public IEnumerable<Book> List()
        {
            return _fakeDb;
        }

        private void PopulateFakeRepository(int count)
        {
            this._fakeDb = new List<Book>();

            for (int i = 1; i <= count; i++)
            {
                this.Insert(new Book($"Livro{i}"));
            }
        }

    }
}
