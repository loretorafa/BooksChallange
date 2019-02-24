using BooksChallange.Domain.Interfaces;
using BooksChallange.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace BooksChallange.Tests.Repositories
{
    public class FakeBookRepository : IBookRepository
    {
        private List<Book> _fakeDb;

        public FakeBookRepository(int itemCount = 0)
        {
            this.PopulateFakeRepository(itemCount);
        }

        public Book Get(int id)
        {
            return _fakeDb.SingleOrDefault(b => b.Id == id);
        }

        public Book Create(Book book)
        {
            book.Id = _fakeDb.Count() + 1;

            _fakeDb.Add(book);

            return Get(book.Id);
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
                this.Create(new Book($"Livro{i}"));
            }
        }

    }
}
