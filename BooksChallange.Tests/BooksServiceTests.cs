using BooksChallange.Application.Services;
using BooksChallange.Application.Validators;
using BooksChallange.Domain.Interfaces;
using BooksChallange.Domain.Models;
using BooksChallange.Tests.Repositories;
using FluentValidation;
using NUnit.Framework;
using System.Linq;

namespace BooksChallange.Tests
{
    public class BookServiceTests
    {
        private IService<Book> _service;
        private int _itemCount = 0;

        [SetUp]
        public void Setup()
        {
            var fakeRepository = new FakeBookRepository(_itemCount);
            this._service = new BaseService<Book>(fakeRepository);
        }

        [Test]
        public void GetById_BooksExists_ReturnsBook()
        {
            const int id = 1;
            SetItemCount(id);

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
        public void List_HasBooks_ReturnsBooks()
        {
            const int itemCount = 5;
            SetItemCount(itemCount);

            var response = _service.List();

            Assert.IsNotNull(response);
            Assert.AreEqual(itemCount, response.ToList().Count);

        }

        [Test]
        public void List_HasNoBooks_ReturnsNoBooks()
        {
            const int itemCount = 0;
            SetItemCount(itemCount);

            var response = _service.List();

            Assert.IsNotNull(response);
            Assert.AreEqual(itemCount, response.ToList().Count);
        }

        private void SetItemCount(int count)
        {
            this._itemCount = count;

            Setup();
        }
    }
}