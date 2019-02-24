using BooksChallange.Domain.Models;
using FluentValidation;
using System.Collections.Generic;

namespace BooksChallange.Domain.Interfaces
{
    public interface IService<T> where T : BaseEntity
    {
        T GetById(int id);

        T Create<V>(T obj) where V : AbstractValidator<T>;

        IEnumerable<T> List();
    }
}
