
using BookCatalog.DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.DataAccess
{
    public static class DbInitializer
    {
        private static BookCatalogDbContext _context = null!;
        private static UserManager<User> _userManager = null!;

        public static void Initialize(BookCatalogDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;

            _context.Database.Migrate();

            if (!_context.Books.Any())
            {
                seedDb();
            }

            if (!_context.Users.Any())
            {
                addTestUser();
            }
        }

        private static void seedDb()
        {
            seedAuthors(out Author[] authors);
            seedGenres(out Genre[] genres);
            seedBooks(out Book[] books, authors, genres);
        }

        private static void seedAuthors(out Author[] authors)
        {
            authors = new[] 
            {
                new Author() { Name = "Dave Eggers" },
                new Author() { Name = "Yuan Ye" },
                new Author() { Name = "Fekete István" }
            };

            _context.Authors.AddRange(authors);
            _context.SaveChanges();
        }
        private static void seedGenres(out Genre[] genres)
        {
            genres = new[]
            {
                new Genre() { Name = "Fantasy" },
                new Genre() { Name = "Sci-Fi" },
                new Genre() { Name = "Historical Fiction" },
                new Genre() { Name = "Romance" },
                new Genre() { Name = "Literary Fiction" },
                new Genre() { Name = "Folklore and Mythos" },
                new Genre() { Name = "Crime and Detective" }
            };

            _context.Genres.AddRange(genres);
            _context.SaveChanges();
        }
        private static void seedBooks(out Book[] books, Author[] authors, Genre[] genres)
        {
            books = new[]
            {
                new Book()
                {
                    Name = "The Parade",
                    Synopsis = "From a beloved author, a spare, powerful story of two men, Western contractors sent to work far from home," +
                                " tasked with paving a road to the capital in a dangerous and largely lawless country.",
                    PublishedAt = new DateTime(2019,3,19),
                    GenreId = genres[4].Id,
                    AuthorId = authors[0].Id
                },
                new Book()
                {
                    Name = "Lord of the Mysteries",
                    Synopsis = "With the rising tide of steam power and machinery, who can come close to being a Beyonder?" +
                                " Shrouded in the fog of history and darkness, who or what is the lurking evil that murmurs into our ears?",
                    PublishedAt = new DateTime(2018,4,1),
                    GenreId = genres[0].Id,
                    AuthorId = authors[1].Id
                },
                new Book()
                {
                    Name = "A Koppányi Aga Testamentuma",
                    Synopsis = "Fekete István első regénye a török hódoltság idején játszódik Somogyban. Főhőse Babocsai László, egy végvári vitéz, akinek édesapját a koppányi aga, Oglu párviadalban legyőzte és megölte." +
                                " László fogadalmat tesz arra, hogy ő viszont a koppányi agát öli meg egy újabb párviadalban.",
                    PublishedAt = new DateTime(1937,1,1),
                    GenreId = genres[2].Id,
                    AuthorId = authors[2].Id
                },
            };

            _context.Books.AddRange(books);
            _context.SaveChanges();
        }
        private static void addTestUser()
        {
            User user = new User() { UserName = "TestUser" };
            string password = "test@123";

            var result = _userManager.CreateAsync(user, password).Result;

            _context.SaveChanges();
        }

    }
}
