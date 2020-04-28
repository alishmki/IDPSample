using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleTemp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new HttpClient();
            client.SetBearerToken("access_token");
            var response = await client.GetAsync("url");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }


        } 
    }
}
