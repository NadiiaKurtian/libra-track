using LibraTrack.Models;
using LibraTrack.Services;
using LibraTrack.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LibraTrack.Controllers;

[Route("api/books")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly BookService _bookService;

    public BooksController(BookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public ActionResult<List<Book>> GetAllBooks() => Ok(_bookService.GetAllBooks());

    [HttpGet("{id:int}")]
    public ActionResult<Book> GetBookById(int id)
    {
        var book = _bookService.GetBookById(id);
        return book is not null ? Ok(book) : NotFound();
    }

    [HttpGet("year/{year:int}")]
    public ActionResult<List<Book>> GetBooksByYear(int year)
    {
        var books = _bookService.GetAllBooks().Where(b => b.Year == year).ToList();
        return books.Any() ? Ok(books) : NotFound();
    }

    [HttpGet("default-year/{year:int?}")]
    public ActionResult<string> GetDefaultYear(int? year = 2000)
    {
        return Ok($"Default Year: {year}");
    }

    [HttpGet("category/{genre}/{year:int}")]
    public ActionResult<string> GetBooksByGenreAndYear(string genre, int year)
    {
        return Ok($"Books in category: {genre}, year: {year}");
    }

    [HttpGet("title/{title:minlength(5)}")]
    public ActionResult<string> GetBookByTitle(string title)
    {
        return Ok($"Book title: {title}");
    }

    [HttpPost]
    public ActionResult AddBook([FromBody] BookDto bookDto)
    {
        var book = new Book { Title = bookDto.Title, Year = bookDto.Year, AuthorId = bookDto.AuthorId };
        _bookService.AddBook(book);
        return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateBook(int id, [FromBody] BookDto bookDto)
    {
        var updated = _bookService.UpdateBook(id, bookDto);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteBook(int id) => _bookService.DeleteBook(id) ? NoContent() : NotFound();
}
