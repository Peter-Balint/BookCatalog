
using BookCatalog.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.DataAccess.Services
{
    public class BooksService
    {
        private readonly BookCatalogDbContext _context;

        public BooksService(BookCatalogDbContext context)
        {
            _context = context;
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books
                .Where(b => b.Id == id)
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .FirstOrDefaultAsync();
        }
        public async Task<IReadOnlyCollection<Book>> GetAllAsync()
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .ToListAsync();
        }
        public async Task AddAsync(Book book)
        {
            if (await bookExistsByDetails(book.Name, book.AuthorId)) { return; }

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            Book? book = await GetByIdAsync(id);

            if (book == null) { return; }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(int id, Book book)
        {
            Book? bookToChange = await GetByIdAsync(id);
            if (bookToChange == null) { return; }

            _context.Update(bookToChange);

            bookToChange.Name = book.Name;
            bookToChange.Synopsis = book.Synopsis;
            bookToChange.PublishedAt = book.PublishedAt;
            bookToChange.AuthorId = book.AuthorId;
            bookToChange.GenreId = book.GenreId;

            await _context.SaveChangesAsync();
        }

        private async Task<bool> bookExistsByDetails(string name, int authorId)
        {
            return await _context.Books
                .Where(b => b.Name == name)
                .Where(b => b.AuthorId == authorId)
                .AnyAsync();
        }
        

    }
}
