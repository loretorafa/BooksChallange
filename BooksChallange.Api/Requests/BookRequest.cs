using BooksChallange.Domain.Entities;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BooksChallange.Api.Requests
{
    public class BookRequest
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        public string Language { get; set; }

        internal Book ToDomain()
        {

            return new Book(this.Title, this.Description, this.ISBN, this.Language);
        }
    }
}
