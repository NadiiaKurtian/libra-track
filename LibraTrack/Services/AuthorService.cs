using LibraTrack.DTOs;
using LibraTrack.Models;

namespace LibraTrack.Services
{
    public class AuthorService
    {
        private readonly List<Author> _authors = new();
        private int _nextId = 1;

        public List<Author> GetAllAuthors() => _authors;

        public Author? GetAuthorById(int id) => _authors.FirstOrDefault(a => a.Id == id);

        public Author AddAuthor(Author author)
        {
            author.Id = _nextId++;
            _authors.Add(author);
            return author;
        }

        public bool UpdateAuthor(int id, AuthorDto authorDto)
        {
            var author = GetAuthorById(id);
            if (author == null) return false;

            author.Name = authorDto.Name;
            author.BirthYear = authorDto.BirthYear;
            return true;
        }

        public bool DeleteAuthor(int id)
        {
            var author = GetAuthorById(id);
            if (author == null) return false;

            _authors.Remove(author);
            return true;
        }
    }
}
