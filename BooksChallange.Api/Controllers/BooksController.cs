using BooksChallange.Api.Requests;
using BooksChallange.Api.Responses;
using BooksChallange.Application.Validators;
using BooksChallange.Domain.Entities;
using BooksChallange.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace BooksChallange.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private IBookService _service;

        public BooksController(IBookService service)
        {
            this._service = service;
        }

        [HttpPost("Book")]
        [Produces("application/JSON")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult Post(BookRequest request)
        {
            var book = _service.Create<BookValidator>(request.ToDomain());

            return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
        }

        [HttpGet("{id}")]
        [Produces("application/JSON")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<Book> GetById(int id)
        {
            var book = _service.GetById(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpGet]
        [Produces("application/JSON")]
        [ProducesResponseType(200)]
        [HttpGet]
        public ActionResult<BooksResponse> Get()
        {
            var response = new BooksResponse(_service.List());

            return Ok(response);
        }
    }
}
