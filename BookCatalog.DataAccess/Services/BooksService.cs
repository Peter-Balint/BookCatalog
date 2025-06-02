
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

        public async Task<Book?> GetBookById(int id)
        {
            return  await _context.Books
                .Where(b => b.Id == id)
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .FirstOrDefaultAsync();
        }
    }
}
