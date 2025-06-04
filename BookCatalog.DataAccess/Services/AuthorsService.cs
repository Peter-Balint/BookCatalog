
using BookCatalog.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.DataAccess.Services
{
    public class AuthorsService
    {
        private readonly BookCatalogDbContext _context;

        public AuthorsService(BookCatalogDbContext context)
        {
            _context = context;
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            return await _context.Authors
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();
        }
        public async Task<IReadOnlyCollection<Author>> GetAllAsync()
        {
            return  await _context.Authors.ToListAsync();
        }

        public async Task AddAsync(Author author)
        {
            if(_context.Authors.Where(a => a.Name == author.Name).Any())
            {
                return;
            }

            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Author? author = await GetByIdAsync(id);

            if (author == null) { return; }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
        }
    }
}
