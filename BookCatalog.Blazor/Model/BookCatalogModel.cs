
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


        //this can still be cool, but it doesn't work
        public async Task ReadListAsync<Dto>(string endPoint, List<Dto> list)
        {
            HttpResponseMessage response = await _client.GetAsync($"api/{endPoint}/");

            if (response.IsSuccessStatusCode)
            {
                list = await response.Content.ReadAsAsync<List<Dto>>();
            }
            else
            {
                throw new Exception("Service returned response: " + response.StatusCode);
            }
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
                //await ReadListAsync("books",Books);
            }
            else
            {
                throw new Exception("Service returned response: " + response.StatusCode);
            }
        }
        public async Task DeleteBookAsync(int id)
        {
            HttpResponseMessage response = await _client.DeleteAsync("api/books/"+id);
            if (response.IsSuccessStatusCode)
            {
                await ReadBooksAsync();
            }
            else
            {
                throw new Exception("Service returned response: " + response.StatusCode);
            }
        }

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
        public async Task UpdateAuthorAsync(int id, AuthorDto author)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync("api/authors/"+id, author);

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
