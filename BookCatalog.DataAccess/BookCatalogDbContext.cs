
using BookCatalog.DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.DataAccess
{
    public class BookCatalogDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public BookCatalogDbContext(DbContextOptions<BookCatalogDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Author> Authors { get; set; } = null!;
        public DbSet<Genre> Genres { get; set; } = null!;  
        public DbSet<Rating> Ratings { get; set; } = null!;
    }
}
