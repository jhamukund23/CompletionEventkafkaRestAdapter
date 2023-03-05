using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CompletionEventkafkaRestAdapter.Application
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        public async Task<string> GetRequestAsync(string requestUri, string token, string baseAddress)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));                 

                    client.BaseAddress = new Uri(baseAddress);

                    var response = await client.GetAsync(requestUri);
                    string responseContent = response.Content.ReadAsStringAsync().Result;

                    if (response.IsSuccessStatusCode)
                    {
                        return responseContent;
                    }

                    throw new Exception(responseContent);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> PutRequestAsync(string requestUri, StringContent content, string token, string baseAddress)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                client.BaseAddress = new Uri(baseAddress);

                var response = await client.PutAsync(requestUri, content);
                string responseContent = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    return responseContent;
                }
                else
                {
                    throw new Exception(responseContent);
                }
            }
        }

        public async Task<bool> DeleteRequestAsync(string requestUri, string token, string baseAddress)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                client.BaseAddress = new Uri(baseAddress);

                var response = await client.DeleteAsync(requestUri);


                return response.IsSuccessStatusCode;
            }
        }

        public async Task<string> PostRequestAsync(string requestUri, StringContent content, string token, string baseAddress)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                client.BaseAddress = new Uri(baseAddress);

                var response = await client.PostAsync(requestUri, content);
                string responseContent = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    return responseContent;
                }
                else
                {
                    throw new Exception(responseContent);
                }
            }
        }

        public async Task<string> PostRequestAsync_BasicAuth(string requestUri, StringContent content, string username, string password, string baseAddress)
        {
            using (var client = new HttpClient())
            {

                var byteArray = Encoding.ASCII.GetBytes(string.Format("{0}:{1}", username, password));

                client.DefaultRequestHeaders.Add($"Authorization", $"Basic {Convert.ToBase64String(byteArray)}");

                client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.BaseAddress = new Uri(baseAddress);

                var response = await client.PostAsync(requestUri, content);
                string responseContent = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    return responseContent;
                }
                else
                {
                    throw new Exception(responseContent);
                }
            }
        }

        public async Task<string> GetRequestAsync_BasicAuth(string requestUri, string username, string password, string baseAddress)
        {
            using (var client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes(string.Format("{0}:{1}", username, password));

                client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add($"Authorization", $"Basic {Convert.ToBase64String(byteArray)}");
                client.BaseAddress = new Uri(baseAddress);

                var response = await client.GetAsync(requestUri);
                string responseContent = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    return responseContent;
                }
                else
                {

                    throw new Exception(responseContent);
                }
            }
        }

    }
}
