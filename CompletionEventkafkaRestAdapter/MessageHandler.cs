using CompletionEventkafkaRestAdapter.Application;
using Kafka.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace CompletionEventkafkaRestAdapter
{
    public class MessageHandler : IKafkaHandler<string, StorageEventHubData>
    {
        private readonly IHttpClientWrapper _httpClientWrapper;
        string baseAddress = "http://localhost:5271/";
        public MessageHandler(IHttpClientWrapper httpClientWrapper)
        {
            _httpClientWrapper = httpClientWrapper;
        }
        public async Task<Task> HandleAsync(string key, StorageEventHubData value)
        {
            // Here we can actually write the code to call microservices
           
            await InvokeService(value);            
            return Task.CompletedTask;
        }

        private async Task InvokeService(StorageEventHubData value)
        {
            var requestURI = "api/AddDocument/GetAddDocumentBlobUrl";
            var jsonBody = JsonConvert.SerializeObject(value, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            var result = await _httpClientWrapper.PostRequestAsync(requestURI, content, "", baseAddress);
        }
    }
}
