using Microsoft.AspNetCore.Mvc;
using LibraTrack.Models;
using LibraTrack.Services;

namespace LibraTrack.Controllers
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

        [HttpGet("{id:int}")]
        public ActionResult<Author> GetAuthorById(int id)
        {
            var author = _authorService.GetAuthorById(id);
            if (author == null) return NotFound();
            return Ok(author);
        }

        [HttpGet("default")]
        public ActionResult<Author> GetDefaultAuthor()
        {
            var defaultAuthor = new Author { Id = 0, Name = "Default Author", BirthYear = 2000 };
            return Ok(defaultAuthor);
        }

        [HttpGet("search/{name:minlength(3)}")]
        public ActionResult<List<Author>> SearchAuthors(string name)
        {
            var authors = _authorService.GetAllAuthors().Where(a => a.Name.Contains(name)).ToList();
            if (!authors.Any()) return NotFound();
            return Ok(authors);
        }

        [HttpPost]
        public ActionResult<Author> CreateAuthor([FromBody] Author newAuthor)
        {
            var createdAuthor = _authorService.AddAuthor(newAuthor);
            return CreatedAtAction(nameof(GetAuthorById), new { id = createdAuthor.Id }, createdAuthor);
        }

        [HttpPut("{id:int}")]
        public ActionResult UpdateAuthor(int id, [FromBody] Author updatedAuthor)
        {
            var success = _authorService.UpdateAuthor(id, updatedAuthor);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeleteAuthor(int id)
        {
            var success = _authorService.DeleteAuthor(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
