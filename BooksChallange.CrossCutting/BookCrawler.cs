using BooksChallange.Domain.Entities;
using BooksChallange.Domain.Interfaces.Repositories;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

namespace BooksChallange.CrossCutting
{
    public class BookCrawler : IBookCrawler
    {
        private Uri booksUrl = new Uri("https://kotlinlang.org/docs/books.html");

        private Regex isbnRegex = new Regex(@"(97(8|9))?\d{9}(\d|X)", RegexOptions.Compiled);

        private Regex newLineRegex = new Regex(@"\r\n?|\n|\t", RegexOptions.Compiled);
        private Regex multipleWhiteSpacesRegex = new Regex(@" {2,}", RegexOptions.Compiled);

        public List<Book> GetBooks()
        {
            var article = GetBody(booksUrl).SelectSingleNode("//article");

            var books = new List<Book>();

            foreach (var node in article.SelectNodes("h2"))
            {
                books.Add(GetBook(node));
            }

            return books;
        }

        private Book GetBook(HtmlNode node)
        {
            string title = "";
            string isbn = "Unavailable";
            string description = "";
            string language = "";

            title = node.InnerText;

            var nextNode = node.NextSibling;
            while (nextNode != null && nextNode.Name != "h2")
            {
                if (nextNode.HasClass("book-lang"))
                {
                    language = nextNode.InnerText;
                }
                else if (nextNode.Name == "a")
                {
                    var bookUrl = nextNode.GetAttributeValue("href", "");

                    if (!String.IsNullOrEmpty(bookUrl))
                        isbn = GetIsbn(bookUrl);
                }
                else if (nextNode.Name == "p")
                {
                    description += newLineRegex.Replace(multipleWhiteSpacesRegex.Replace(nextNode.InnerText, " "), string.Empty);
                }
                nextNode = nextNode.NextSibling;
            }

            return new Book(title, description, isbn, language);
        }

        private string GetIsbn(string bookUrl)
        {
            var bookHtml = getBodyString(new Uri(bookUrl));

            var isbn = isbnRegex.Match(bookHtml).ToString();

            return !string.IsNullOrEmpty(isbn) ? isbn : "Unavailable";
        }

        private HtmlNode GetBody(Uri url)
        {
            var page = new HtmlWeb()
            {
                PreRequest = request =>
                {
                    request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                    return true;
                }
            };

            return page.Load(url).DocumentNode.SelectSingleNode("//body");
        }

        private string getBodyString(Uri url)
        {
            var bodyString = GetBody(url).InnerText;

            return newLineRegex.Replace(bodyString, string.Empty);
        }
    }
}
