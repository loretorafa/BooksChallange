using BooksChallange.Domain.Entities;
using BooksChallange.Domain.Interfaces.Repositories;
using BooksChallange.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace BooksChallange.Application.Services
{
    public class BookService : BaseService<Book>, IBookService
    { 
        public BookService(IBookRepository repository) : base(repository){}

        public override IEnumerable<Book> List()
        {
            throw new NotImplementedException();
        }
    }
}
