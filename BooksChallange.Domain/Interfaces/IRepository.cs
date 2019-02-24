using BooksChallange.Domain.Models;
using System.Collections.Generic;

namespace BooksChallange.Domain.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Select(int id);

        T Insert(T book);

        IEnumerable<T> List();
    }
}
