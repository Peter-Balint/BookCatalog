
using BookCatalog.Shared.DTOs;

namespace BookCatalog.Blazor.Model
{
    public class BookCatalogModel
    {
        private HttpClient _client;

        public List<BookDto> Books { get; private set; } = new List<BookDto>();

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

    }
}
