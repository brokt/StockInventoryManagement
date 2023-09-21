using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public class ApiExceptionSerializer : IHttpContentSerializer
    {
        public async Task<T> DeserializeAsync<T>(HttpContent content)
        {
            var contentString = await content.ReadAsStringAsync();
            var apiException = JsonSerializer.Deserialize<RefitApiException>(contentString);
            return (T)Convert.ChangeType(apiException, typeof(T));
        }

        public Task<T?> FromHttpContentAsync<T>(HttpContent content, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public string GetFieldNameForProperty(PropertyInfo propertyInfo)
        {
            throw new NotImplementedException();
        }

        public Task<HttpContent> SerializeAsync<T>(T item)
        {
            throw new NotImplementedException();
        }

        public HttpContent ToHttpContent<T>(T item)
        {
            throw new NotImplementedException();
        }
    }
}
