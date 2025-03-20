using LibraTrack.Models;

namespace LibraTrack.Services
{
    public class AuthorService
    {
        private readonly List<Author> _authors = new();

        public List<Author> GetAllAuthors() => _authors;

        public Author? GetAuthorById(int id) => _authors.FirstOrDefault(a => a.Id == id);

        public Author AddAuthor(Author author)
        {
            author.Id = _authors.Any() ? _authors.Max(a => a.Id) + 1 : 1;
            _authors.Add(author);
            return author;
        }

        public bool UpdateAuthor(int id, Author updatedAuthor)
        {
            var author = _authors.FirstOrDefault(a => a.Id == id);
            if (author == null) return false;

            author.Name = updatedAuthor.Name;
            author.BirthYear = updatedAuthor.BirthYear;
            return true;
        }

        public bool DeleteAuthor(int id)
        {
            var author = _authors.FirstOrDefault(a => a.Id == id);
            if (author == null) return false;

            _authors.Remove(author);
            return true;
        }
    }
}
