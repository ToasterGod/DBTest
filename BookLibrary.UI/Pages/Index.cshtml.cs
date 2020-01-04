using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BookLibrary.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BookLibrary.UI.Pages
{
    public class IndexModel : PageModel
    {
        //bindproperty makes the property vidible for the actual page(index.cshtml)
        [BindProperty(SupportsGet = true)]
        public IEnumerable<Author> Authors { get; set; }

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        //onget is called by webbrowser to get the page(OnPost, OnPut, OnDelete)
        public async Task OnGet()
        {
            _logger.LogInformation("running OnGet");
            using (var client = new HttpClient())
            {
                var text = await ReadAuthors(client);
                Authors = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Author>>(text, new System.Text.Json.JsonSerializerOptions
                {PropertyNameCaseInsensitive = true});
                _logger.LogInformation($"Authors:\n{text}");

                //var response = await AddAuthor(client);
                //Console.WriteLine($"Added author status:\n\t{response.StatusCode}");
            }
        }

        private static async Task<string> ReadAuthors(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var stringTask = client.GetStringAsync("https://localhost:5001/authors");

            return await stringTask;
        }

        private static async Task<HttpResponseMessage> AddAuthor(HttpClient client)
        {
            string author = "{\"name\": \"Anne Frank\"}";

            HttpContent httpContent = new StringContent(author, Encoding.UTF8);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync("https://localhost:5001/authors", httpContent);
            return response;
        }
    }
}
