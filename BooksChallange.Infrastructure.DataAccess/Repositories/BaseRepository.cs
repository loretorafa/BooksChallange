using BooksChallange.Domain.Interfaces;
using BooksChallange.Domain.Entities;
using BooksChallange.Infrastructure.DataAccess.Context;
using System.Collections.Generic;

namespace BooksChallange.Infrastructure.DataAccess.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private BooksChallangeContext _context;

        public BaseRepository()
        {
            this._context = new BooksChallangeContext();
        }

        public BaseRepository(BooksChallangeContext context)
        {
            this._context = context;
        }

        public T Select(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public T Insert(T obj)
        {
            _context.Set<T>().Add(obj);
            _context.SaveChanges();

            return this.Select(obj.Id);
        }

        public IEnumerable<T> List()
        {
            return _context.Set<T>();
        }

        public void Delete(int id)
        {
            _context.Set<T>().Remove(Select(id));
            _context.SaveChanges();
        }
    }
}