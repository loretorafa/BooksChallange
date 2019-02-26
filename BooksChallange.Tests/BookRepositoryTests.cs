using BooksChallange.Domain.Entities;
using BooksChallange.Domain.Interfaces.Repositories;
using BooksChallange.Infrastructure.DataAccess.Repositories;
using NUnit.Framework;
using System.Linq;
using BooksChallange.Infrastructure.DataAccess.Context;

namespace Tests
{
    public class BookRepositoryTests
    {
        private IBookRepository _repository;

        [SetUp]
        public void Setup()
        {
            this._repository = new BookRepository(new BooksChallangeContext());

            ClearRepository();
        }

        [Test]
        public void Insert_ValidBook_ReturnsBook()
        {
            const string title = "Novo Livro";
            const string description = "bla bla bla";
            const string isbn = "3493284932";
            const string language = "BR";
            var book = new Book(title, description, isbn, language);

            var response = _repository.Insert(book);

            Assert.IsNotNull(response);
            Assert.AreSame(typeof(Book), response.GetType());
            Assert.AreEqual(title, response.Title);
        }

        [Test]
        public void Select_BooksExists_ReturnsBook()
        {
            const int itemCount = 1;
            PopulateRepository(itemCount);
            var id = _repository.List().FirstOrDefault().Id;

            var response = _repository.Select(id);

            Assert.IsNotNull(response);
            Assert.AreSame(typeof(Book), response.GetType());
            Assert.AreEqual(id, response.Id);
        }

        [Test]
        public void Select_BooksDoesNotExist_ReturnsNull()
        {
            const int id = 0;

            var response = _repository.Select(id);

            Assert.IsNull(response);
        }

        [Test]
        public void List_HasBooks_ReturnsBooks()
        {
            const int itemCount = 5;
            PopulateRepository(itemCount);

            var response = _repository.List();

            Assert.IsNotNull(response);
            Assert.AreEqual(itemCount, response.ToList().Count);

        }

        [Test]
        public void List_HasNoBooks_ReturnsNoBooks()
        {
            const int itemCount = 0;
            PopulateRepository(itemCount);

            var response = _repository.List();

            Assert.IsNotNull(response);
            Assert.AreEqual(itemCount, response.ToList().Count);
        }

        private void PopulateRepository(int count)
        {
            for (int i = 1; i <= count; i++)
            {
                _repository.Insert(new Book($"Livro{i}"));
            }
        }

        private void ClearRepository()
        {
            var currentBooks = _repository.List();

            foreach (var book in currentBooks)
            {
                _repository.Delete(book.Id);
            }
        }
    }
}