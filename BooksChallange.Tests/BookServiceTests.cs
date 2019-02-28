using BooksChallange.Application.Services;
using BooksChallange.Application.Validators;
using BooksChallange.Domain.Entities;
using BooksChallange.Domain.Interfaces.Services;
using BooksChallange.Domain.Interfaces.Repositories;
using BooksChallange.Infrastructure.DataAccess.Repositories;
using FluentValidation;
using NUnit.Framework;
using System.Linq;
using BooksChallange.Infrastructure.DataAccess.Context;
using BooksChallange.CrossCutting;

namespace Tests
{
    public class BookServiceTests
    {
        private IBookService _service;
        private IBookRepository _repository;
        private IBookCrawler _crawler;

        [SetUp]
        public void Setup()
        {
            this._repository = new BookRepository(new BooksChallangeContext());
            this._crawler = new BookCrawler();
            this._service = new BookService(_repository, _crawler);

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
            var id = _repository.List().FirstOrDefault().Id;

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
        public void List_ConnectsToExternalDataSource_ReturnsExpectedBookCount()
        {
            const int expectedBooks = 30;
            var response = _service.List().ToList();

            Assert.IsNotNull(response);
            Assert.AreEqual(expectedBooks, response.Count());

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