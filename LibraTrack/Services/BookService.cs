using LibraTrack.Models;

namespace LibraTrack.Services
{
    public class BookService
    {
        private readonly List<Book> _books = new List<Book>();
        public List<Book> GetAllBooks() => _books;
        public Book? GetBookById(int id) => _books.FirstOrDefault(x => x.Id == id);
        public void AddBok(Book book) => _books.Add(book);
        public bool Remove(int id)
        {
            var book = GetBookById(id);
            if (book == null) return false;
            _books.Remove(book);
            return true;
        }
    }
}
