using BooksChallange.Domain.Entities;
using BooksChallange.Domain.Interfaces.Repositories;
using BooksChallange.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace BooksChallange.Application.Services
{
    public class BookService : BaseService<Book>, IBookService
    {
        private IBookCrawler _crawler;

        public BookService(IBookRepository repository, IBookCrawler crawler) : base(repository)
        {
            this._crawler = crawler;
        }

        public override IEnumerable<Book> List()
        {
            return _crawler.GetBooks();
        }
    }
}
