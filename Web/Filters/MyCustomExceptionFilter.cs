using Core.Extensions;
using Core.Flash;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Refit;
using System.Text;

namespace Web.Filters
{
    public class MyCustomExceptionFilter : IExceptionFilter
    {
        private readonly IFlasher flasher;
        public MyCustomExceptionFilter(IFlasher flasher)
        {
            this.flasher = flasher;
        }
        public void OnException(ExceptionContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (context.Exception is (RefitApiException))
            {
                var errorMessages = context.Exception.Message.Split("--");
                foreach (var item in errorMessages)
                {
                    stringBuilder.Append(item);
                }
                flasher.Danger(stringBuilder.ToString(),true);
                string referUrl = context.HttpContext.Request.Headers["Referer"].ToString();
                context.Result = new RedirectResult(!string.IsNullOrEmpty(referUrl) ? referUrl : "/Error");
                context.ExceptionHandled = true;
            }
        }
    }
}
