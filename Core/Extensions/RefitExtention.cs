using Refit;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class RefitExtention
    {
        public static async Task<T> GetDataOrThrow<T>(this Task<IApiResponse<T>> responseTask)
        {
            var response = await responseTask.ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                var apiException = response.Error;
                throw new RefitApiException(apiException.Content,apiException.StatusCode);
                //var customException = CustomExceptionFactory.CreateException(apiException);
                //throw customException;
            }

            return response.Content;
        }
    }
}
