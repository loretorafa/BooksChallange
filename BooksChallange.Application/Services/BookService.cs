using BooksChallange.Application.Validators;
using BooksChallange.Domain.Interfaces;
using BooksChallange.Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BooksChallange.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;
        private readonly IValidator _validator;

        public BookService(IBookRepository repository)
        {
            this._repository = repository;
            this._validator = new BookValidator();
        }

        public Book GetById(int id)
        {
            return _repository.Get(id);
        }

        public Book Create(string title = "", string description = "", string isbn = "", string language = "")
        {
            var item = new Book(title, description, isbn, language);

            if (_validator.Validate(item).IsValid)
            {
                return _repository.Create(item);
            }

            return null;
        }

        public IEnumerable<Book> List()
        {
            return _repository.List();
        }
    }
}
