
using BookCatalog.Shared.DTOs;
using System.Runtime.InteropServices;

namespace BookCatalog.Blazor.Model
{
    public class BookCatalogModel
    {
        private HttpClient _client;

        public List<BookDto> Books { get; private set; } = new List<BookDto>();
        public List<AuthorDto> Authors { get; private set; } = new List<AuthorDto>();
        public List<GenreDto> Genres { get; private set; } = new List<GenreDto>();

        public BookCatalogModel(HttpClient httpClient)
        {
            _client = httpClient;
        }

        public async Task ReadBooksAsync()
        {
            HttpResponseMessage response = await _client.GetAsync("api/books/");
                
            if (response.IsSuccessStatusCode)
            {
                Books = await response.Content.ReadAsAsync<List<BookDto>>();
            }
            else
            {
                throw new Exception("Service returned response: " + response.StatusCode);
            }
        }
        public async Task AddBookAsync(BookRequestDto book)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/books/",book);

            if (response.IsSuccessStatusCode)
            {
                await ReadBooksAsync();
            }
            else
            {
                throw new Exception("Service returned response: " + response.StatusCode);
            }
        }

        //todo: if i have time, refactor all these read functions into singular readlist with string and generic dto type parameters
        public async Task ReadAuthorsAsync() 
        {
            HttpResponseMessage response = await _client.GetAsync("api/authors/");

            if (response.IsSuccessStatusCode)
            {
                Authors = await response.Content.ReadAsAsync<List<AuthorDto>>();
            }
            else
            {
                throw new Exception("Service returned response: " + response.StatusCode);
            }
        }
        public async Task AddAuthorAsync(AuthorDto author)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/authors/", author);

            if (response.IsSuccessStatusCode)
            {
                await ReadAuthorsAsync();
            }
            else
            {
                throw new Exception("Service returned response: " + response.StatusCode);
            }
        }

        public async Task ReadGenresAsync()
        {
            HttpResponseMessage response = await _client.GetAsync("api/genres/");

            if (response.IsSuccessStatusCode)
            {
                Genres = await response.Content.ReadAsAsync<List<GenreDto>>();
            }
            else
            {
                throw new Exception("Service returned response: " + response.StatusCode);
            }
        }
        public async Task AddGenreAsync(GenreDto genre)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/genres/", genre);

            if (response.IsSuccessStatusCode)
            {
                await ReadGenresAsync();
            }
            else
            {
                throw new Exception("Service returned response: " + response.StatusCode);
            }
        }
    }
}
