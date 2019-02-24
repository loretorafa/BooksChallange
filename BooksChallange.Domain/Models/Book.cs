namespace BooksChallange.Domain.Models
{
    public class Book
    {
        public Book(string title = "", string description = "", string isbn = "", string language = "")
        {
            this.Title = title;
            this.Description = description;
            this.ISBN = isbn;
            this.Language = language;
        }

        public int Id { get; set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string ISBN { get; private set; }
        public string Language { get; private set; }
    }
}
