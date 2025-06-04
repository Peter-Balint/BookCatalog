
using BookCatalog.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.DataAccess.Services
{
    public class GenresService
    {
        private readonly BookCatalogDbContext _context;

        public GenresService(BookCatalogDbContext context)
        {
            _context = context;
        }

        public async Task<Genre?> GetByIdAsync(int id)
        {
            return await _context.Genres
                .Where(g => g.Id == id)
                .FirstOrDefaultAsync();
        }
        public async Task<IReadOnlyCollection<Genre>> GetAllAsync()
        {
            return await _context.Genres.ToListAsync();
        }

        public async Task AddAsync(Genre genre)
        {
            if (_context.Genres.Where(g => g.Name == genre.Name).Any())
            {
                return;
            }

            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Genre? genre = await GetByIdAsync(id);

            if (genre == null) { return; }

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
        }
    }
}
