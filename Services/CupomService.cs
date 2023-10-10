using Newtonsoft.Json;
using OpenStore.Models;
using System;
using System.Net.Http;
using System.Text;

namespace OpenStore.Services
{
    public class CupomService
    {

        public string BaseURL { get; set; }

        public CupomService(string baseURL)
        {
            BaseURL = baseURL;
        }

        public async void SaveCupom(Cupom cupom)
        {
            string uri = BaseURL;
            using var client = new HttpClient();

            var content = new StringContent(JsonConvert.SerializeObject(cupom), Encoding.UTF8, "application/json");
            using var response = await client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                PostResponse postResponse = JsonConvert.DeserializeObject<PostResponse>(responseContent);
                cupom.Id = long.Parse(postResponse.Id);
            }else
            {
                  throw new Exception("Não foi possível salvar o cupom");
            }
        }

        public class PostResponse
        {
            public string Id { get; set; }
        }

    }
}
