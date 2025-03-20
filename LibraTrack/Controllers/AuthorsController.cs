using Microsoft.AspNetCore.Mvc;
using BookApi.Services;
using LibraTrack.Models;
using LibraTrack.DTOs;

namespace BookApi.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly AuthorService _authorService;

        public AuthorsController(AuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public ActionResult<List<Author>> GetAuthors() => Ok(_authorService.GetAllAuthors());

        [HttpGet("{id}")]
        public ActionResult<Author> GetAuthorById(int id)
        {
            var author = _authorService.GetAuthorById(id);
            if (author == null) return NotFound();
            return Ok(author);
        }

        [HttpPost]
        public ActionResult<Author> CreateAuthor([FromBody] AuthorDto authorDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var newAuthor = new Author { Name = authorDto.Name, BirthYear = authorDto.BirthYear };
            _authorService.AddAuthor(newAuthor);
            return CreatedAtAction(nameof(GetAuthorById), new { id = newAuthor.Id }, newAuthor);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, [FromBody] AuthorDto authorDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!_authorService.UpdateAuthor(id, authorDto)) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            if (!_authorService.DeleteAuthor(id)) return NotFound();
            return NoContent();
        }
    }
}
