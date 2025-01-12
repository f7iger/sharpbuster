using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SharpBuster
{
    class Requisicao
    {
        public async Task<int> GetReq(string url){
            string u = url;
            HttpClient client = new HttpClient();
            try {
                HttpResponseMessage response = await client.GetAsync(u);
                int statusCode = (int)response.StatusCode;
                if (statusCode == 200){
                    Console.WriteLine($"{statusCode} Found! at {url}");
                    return statusCode;
                } else if (statusCode == 404){
                    Console.WriteLine($"{statusCode} Not Found");
                    return statusCode;
                } else {
                    Console.WriteLine($"Erro: {statusCode}");
                    return statusCode;
                }
            } catch (HttpRequestException e){
                Console.WriteLine($"Erro: {e.Message}");
                return 1;
            }
        }
    }
}