using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompletionEventkafkaRestAdapter.Application
{
    public interface IHttpClientWrapper
    {
        Task<bool> DeleteRequestAsync(string requestUri, string token, string baseAddress);
        Task<string> GetRequestAsync(string requestUri, string token, string baseAddress);      
        Task<string> PutRequestAsync(string requestUri, StringContent content, string token, string baseAddress);
        Task<string> PostRequestAsync(string requestUri, StringContent content, string token, string baseAddress);

        Task<string> PostRequestAsync_BasicAuth(string requestUri, StringContent content, string username, string password, string baseAddress);
        Task<string> GetRequestAsync_BasicAuth(string requestUri, string username, string password, string baseAddress);
    }
}
