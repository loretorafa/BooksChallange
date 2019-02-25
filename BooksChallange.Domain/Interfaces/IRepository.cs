using BooksChallange.Domain.Entities;
using System.Collections.Generic;

namespace BooksChallange.Domain.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Select(int id);

        T Insert(T obj);

        IEnumerable<T> List();

        void Delete(int id);
    }
}
