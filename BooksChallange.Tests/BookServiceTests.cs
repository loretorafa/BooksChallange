using BooksChallange.Application.Services;
using BooksChallange.Application.Validators;
using BooksChallange.Domain.Entities;
using BooksChallange.Domain.Interfaces;
using BooksChallange.Infrastructure.DataAccess.Repositories;
using FluentValidation;
using NUnit.Framework;
using System.Linq;

namespace Tests
{
    public class BookServiceTests
    {
        private IService<Book> _service;
        private IRepository<Book> _repository;

        [SetUp]
        public void Setup()
        {
            this._repository = new BookRepository();
            this._service = new BookService(_repository);

            ClearRepository();
        }

        [Test]
        public void Create_ValidBook_ReturnsBook()
        {
            const string title = "Novo Livro";
            const string description = "bla bla bla";
            const string isbn = "3493284932";
            const string language = "BR";
            var book = new Book(title, description, isbn, language);

            var response = _service.Create<BookValidator>(book);

            Assert.IsNotNull(response);
            Assert.AreSame(typeof(Book), response.GetType());
            Assert.AreEqual(title, response.Title);
        }

        [Test]
        public void Create_InvalidBook_ThrowsValidationException()
        {
            const string title = "Novo Livro";
            var book = new Book(title);

            TestDelegate testDelegate = new TestDelegate(() => _service.Create<BookValidator>(book));

            Assert.Throws<ValidationException>(testDelegate);
        }

        [Test]
        public void GetById_BooksExists_ReturnsBook()
        {
            const int itemCount = 1;
            PopulateRepository(itemCount);
            var id = _service.List().FirstOrDefault().Id;

            var response = _service.GetById(id);

            Assert.IsNotNull(response);
            Assert.AreSame(typeof(Book), response.GetType());
            Assert.AreEqual(id, response.Id);
        }

        [Test]
        public void GetById_BooksDoesNotExist_ReturnsNull()
        {
            const int id = 0;

            var response = _service.GetById(id);

            Assert.IsNull(response);
        }

        [Test]
        public void List_HasBooks_ReturnsBooks()
        {
            const int itemCount = 5;
            PopulateRepository(itemCount);

            var response = _service.List();

            Assert.IsNotNull(response);
            Assert.AreEqual(itemCount, response.ToList().Count);

        }

        [Test]
        public void List_HasNoBooks_ReturnsNoBooks()
        {
            const int itemCount = 0;
            PopulateRepository(itemCount);

            var response = _service.List();

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