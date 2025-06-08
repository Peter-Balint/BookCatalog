
using BookCatalog.Shared.DTOs;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.InteropServices;

namespace BookCatalog.Blazor.Model
{
    public class BookCatalogModel
    {
        private HttpClient _client;

        public List<BookDto> Books { get; private set; } = new List<BookDto>();
        public List<AuthorDto> Authors { get; private set; } = new List<AuthorDto>();
        public List<GenreDto> Genres { get; private set; } = new List<GenreDto>();

        public Boolean IsUserLoggedIn { get; private set; }
        public UserDto? User { get; private set; }

        public BookCatalogModel(HttpClient httpClient)
        {
            _client = httpClient;
            IsUserLoggedIn = false;
            User = null;
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
        public async Task UpdateBookAsync(int id, BookRequestDto book)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync("api/books/" + id, book);

            if (response.IsSuccessStatusCode)
            {
                await ReadBooksAsync();
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
        public async Task UpdateGenreAsync(int id, GenreDto genre)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync("api/genres/" + id, genre);

            if (response.IsSuccessStatusCode)
            {
                await ReadGenresAsync();
            }
            else
            {
                throw new Exception("Service returned response: " + response.StatusCode);
            }
        }

        public async Task<Boolean> LoginAsync(string userName, string userPassword, bool useCookies = true)
        {
            IsUserLoggedIn = useCookies
                ? await LoginAsync(userName, userPassword)
                : await LoginTokenAsync(userName, userPassword);

            return IsUserLoggedIn;
        }

        public async Task<Boolean> LoginAsync(string userName, string password)
        {

            HttpResponseMessage response = await _client.PostAsJsonAsync("api/users/login", new LoginDto
            {
                UserName = userName,
                Password = password
            });

            if (response.IsSuccessStatusCode)
            {
                User = response.Content.ReadFromJsonAsync<UserDto>().Result;
            }

            return response.IsSuccessStatusCode;
            
        }
        public async Task<Boolean> LoginTokenAsync(string userName, string password)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/users/login-token", new LoginDto
            {
                UserName = userName,
                Password = password
            });

            if (response.IsSuccessStatusCode &&
                response.Headers.TryGetValues("X-Access-Token", out var values))
            {
                string? token = values.FirstOrDefault();
                if (token != null)
                {
                    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    User = response.Content.ReadFromJsonAsync<UserDto>().Result;
                }
            }

            return response.IsSuccessStatusCode;
        }

        public async Task<Boolean> RegisterAsync(LoginDto loginDto)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/users/", loginDto);

            if (response.IsSuccessStatusCode)
            {
                User = response.Content.ReadFromJsonAsync<UserDto>().Result;
            }
            return response.IsSuccessStatusCode;
        }

        public async Task<Boolean> LogoutAsync()
        {
            if (!IsUserLoggedIn)
                return true;

            HttpResponseMessage response = await _client.PostAsync("api/users/logout",null);
            IsUserLoggedIn = !response.IsSuccessStatusCode;

            return !IsUserLoggedIn;
        }
    }
}
