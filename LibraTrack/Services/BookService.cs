using LibraTrack.DTOs;
using LibraTrack.Models;

namespace LibraTrack.Services
{
    public class BookService
    {
        private readonly List<Book> _books = new();
        private int _nextId = 1;

        public List<Book> GetAllBooks() => _books;

        public Book GetBookById(int id) => _books.FirstOrDefault(b => b.Id == id);

        public Book AddBook(Book book)
        {
            book.Id = _nextId++;
            _books.Add(book);
            return book;
        }

        public bool UpdateBook(int id, BookDto bookDto)
        {
            var book = GetBookById(id);
            if (book == null) return false;

            book.Title = bookDto.Title;
            book.Year = bookDto.Year;
            book.AuthorId = bookDto.AuthorId;
            return true;
        }

        public bool DeleteBook(int id)
        {
            var book = GetBookById(id);
            if (book == null) return false;

            _books.Remove(book);
            return true;
        }
    }
}
