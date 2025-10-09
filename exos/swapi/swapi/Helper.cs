using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace swapi
{
    public class Helper
    {
        static HttpClient client = new HttpClient();
        static public string HttpGet(string query)
        {
            var json = HttpGetAsync(query).ConfigureAwait(false).GetAwaiter().GetResult();
            return json;
        }
        static public async Task<string> HttpGetAsync(string query)
        {
            var response = await client.GetAsync(query.Contains("https") ? query : "https://swapi.dev/api/" + query);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();

            return json;

        }
    }
}
