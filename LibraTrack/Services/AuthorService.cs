using LibraTrack.Models;

namespace LibraTrack.Services
{
    public class AuthorService
    {
        private readonly List<Author> _authors = new List<Author>();
        public List<Author> GetAllAuthors() => _authors;
        public Author? GetAuthorById(int id) => _authors.FirstOrDefault(x => x.Id == id);
        public void AddAuthor(Author author) => _authors.Add(author);
        public bool RemoveAuthor(int id)
        {
            var author = GetAuthorById(id);
            if (author == null) return false;
            _authors.Remove(author);
            return true;
        }
    }
}
