using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClientConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {

            //discover endpoints from metadata
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }


            //request token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest() 
            { 
                Address = disco.TokenEndpoint,
                ClientId = "client",
                ClientSecret = "secret001",
                //Scope = "api1"
            });
            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }
            Console.WriteLine(tokenResponse.Json);


            //call api
            await CallApi(tokenResponse, "http://localhost:5001/identity");
        


            while (true)
            {
                Console.WriteLine("--------------------------");
                Console.WriteLine("1: identity");
                Console.WriteLine("2: get1");
                Console.WriteLine("3: get2");
                Console.WriteLine("Please Enter:");
                var key = Console.ReadKey();
                switch (key.KeyChar)
                {
                    case '1':
                        await CallApi(tokenResponse, "http://localhost:5001/identity");
                        break;
                    case '2':
                        await CallApi(tokenResponse, "http://localhost:5001/get1");
                        break;
                    case '3':
                        await CallApi(tokenResponse, "http://localhost:5001/get2");
                        break;
                    default:
                        Console.WriteLine("Invalid argument");
                        break;
                }
            }
       
        }


        private static async Task CallApi(TokenResponse token,string url)
        {
            var client = new HttpClient();
            client.SetBearerToken(token.AccessToken);
            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
        }
    }
}
