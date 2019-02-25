using BooksChallange.Domain.Interfaces;
using BooksChallange.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;

namespace BooksChallange.Application.Services
{
    public class BaseService<T> : IService<T> where T : BaseEntity
    {
        private readonly IRepository<T> _repository;

        public BaseService(IRepository<T> repository)
        {
            this._repository = repository;
        }

        public T GetById(int id)
        {
            return _repository.Select(id);
        }

        public T Create<V>(T obj) where V : AbstractValidator<T>
        {
            Validate(obj, Activator.CreateInstance<V>());

            _repository.Insert(obj);
            return obj;
        }


        public IEnumerable<T> List() => _repository.List();


        private void Validate(T obj, AbstractValidator<T> validator)
        {
            if (obj != null)
                validator.ValidateAndThrow(obj);
        }
    }
}
