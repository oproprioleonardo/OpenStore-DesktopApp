using Newtonsoft.Json;
using OpenStore.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenStore.Services
{
    public class ProductService
    {

        public string BaseURL { get; set; }

        public ProductService(string baseURL)
        {
            BaseURL = baseURL;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            string uri = BaseURL + "/list";
            using var client = new HttpClient();
            using var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string produtos = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Product[]>(produtos).ToList();
            }
            else
                return new List<Product>();
        }

        public async Task<Product> GetProductByCode(string code)
        {
            string uri = BaseURL + "?code=" + code;
            using var client = new HttpClient();
            using var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string produto = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Product>(produto);
            }
            else
            {
                throw new NotFoundException(typeof(Product), code);
            }
        }

        public async Task<Product> SearchProduct(string terms)
        {
            string uri = BaseURL + "?search=" + terms;
            using var client = new HttpClient();
            using var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string produto = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Product>(produto);
            }
            else
            {
                throw new NotFoundException(typeof(Product), terms);
            }
        }

        public async void SaveProduct(Product product)
        {
            string uri = BaseURL;
            using var client = new HttpClient();

            var content = new StringContent(JsonConvert.SerializeObject(product), System.Text.Encoding.UTF8, "application/json");
            using var response = await client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                string idJson = await response.Content.ReadAsStringAsync();
                PostResponse postResponse = JsonConvert.DeserializeObject<PostResponse>(idJson);
                product.Id = postResponse.Id;
            }
            else
            {
                ErrorResponse errorResp = await response.Content.ReadAsAsync<ErrorResponse>();
                if (errorResp.errors.Count > 0)
                {
                    foreach (Error error in errorResp.errors)
                    {
                        MessageBox.Show(error.Message);
                    }
                    return;
                }
                MessageBox.Show(errorResp.Message ?? "Não foi possível salvar o produto");
            }
        }

        public async void UpdateProduct(Product product)
        {
            string URI = BaseURL + "/" + product.Id;
            using var client = new HttpClient();
            using var response = await client.PutAsJsonAsync(URI, product);

            if (!response.IsSuccessStatusCode)
            {
                ErrorResponse errorResp = await response.Content.ReadAsAsync<ErrorResponse>();
                if (errorResp.errors.Count > 0)
                {
                    foreach (Error error in errorResp.errors)
                    {
                        MessageBox.Show(error.Message);
                    }
                    return;
                }
                MessageBox.Show(errorResp.Message ?? "Não foi possível salvar o produto");
            }
        }

        public async void DeleteProductById(long id)
        {
            string URI = BaseURL + "/" + id;
            using var client = new HttpClient();
            using var response = await client.DeleteAsync(URI);
            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show("Não foi possível excluir o produto");
            }
        }


        public class PostResponse
        {
            public long Id { get; set; }
        }

        [JsonObject]
        public class ErrorResponse
        {
            [JsonProperty("exception")]
            public string Exception { get; set; }
            [JsonProperty("message")]
            public string Message { get; set; }
            [JsonProperty("error")]
            public string Error { get; set; }
            [JsonProperty("stackTrace")]
            public string StrackTrace { get; set; }
            [JsonProperty("errors")]
            public List<Error> errors { get; set; }
        }

        [JsonObject]
        public class Error
        {
            [JsonProperty("message")]
            public string Message { get; set; }
        }

    }
}
