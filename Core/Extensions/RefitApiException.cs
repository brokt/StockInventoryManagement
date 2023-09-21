using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public class RefitApiException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public string ErrorContent { get; }

        public RefitApiException(string message, HttpStatusCode statusCode, string errorContent)
            : base(message)
        {
            StatusCode = statusCode;
            ErrorContent = errorContent;
        }

        public RefitApiException(string message, HttpStatusCode statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }

}
