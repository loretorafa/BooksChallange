using BooksChallange.Domain.Models;
using FluentValidation;

namespace BooksChallange.Application.Validators
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty();
            RuleFor(x => x.Description).NotNull().NotEmpty();
            RuleFor(x => x.ISBN).NotNull().NotEmpty();
            RuleFor(x => x.Language).NotNull().NotEmpty();
        }
    }
}
