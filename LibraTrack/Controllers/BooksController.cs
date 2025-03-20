using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LibraTrack.Models;
using LibraTrack.DTOs;
using LibraTrack.Services;

namespace LibraTrack.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;
        public BooksController(BookService bookService)
        => _bookService = bookService;

        [HttpGet]
        public ActionResult<List<Book>> GetBooks() => Ok(_bookService.GetAllBooks());

        [HttpGet("{id}")]
        public ActionResult<Book> GetBookById(int id)
        {
            var book = _bookService.GetBookById(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public ActionResult<Book> CreateBook([FromBody] BookDto bookDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var newBook = new Book { Title = bookDto.Title, Year = bookDto.Year, AuthorId = bookDto.AuthorId };
            _bookService.AddBook(newBook);
            return CreatedAtAction(nameof(GetBookById), new { id = newBook.Id }, newBook);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] BookDto bookDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!_bookService.UpdateBook(id, bookDto)) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            if (!_bookService.DeleteBook(id)) return NotFound();
            return NoContent();
        }
    }

}
