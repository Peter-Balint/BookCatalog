

using BookCatalog.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.DataAccess.Services
{
    public class RatingsService
    {
        private readonly BookCatalogDbContext _context;

        public RatingsService(BookCatalogDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<Rating>> GetByBookIdAsync(int id)
        {
            return await _context.Ratings
                .Where(r => r.BookId == id)
                .Include(r => r.User)
                .ToListAsync();
        }

        public async Task AddAsync(Rating rating)
        {
             Rating? ratingExists = await _context.Ratings
                .Where(r => r.BookId ==  rating.BookId)
                .Where(r => r.UserId == rating.UserId)
                .FirstOrDefaultAsync();

            if (ratingExists != null)
            {
                _context.Ratings.Remove(ratingExists);
                await _context.SaveChangesAsync();
            }

            await _context.Ratings.AddAsync(rating);

            await _context.SaveChangesAsync();
        }
    }
}
