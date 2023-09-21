using Core.Extensions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Web.Infrastructure
{
    
    public class PageBaseModel : PageModel
    {
  //      private readonly IConfiguration _configuration;
		//private readonly HttpClient httpClient;
		//public PageBaseModel(IConfiguration configuration)
  //      {
  //          _configuration = configuration;
		//	//httpClient = new HttpClient();
		//	//httpClient.BaseAddress = new Uri(_configuration.GetSection("BaseUrl").Get<string>());
		//	//httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",User != null ? User.GetToken() :"");
		//}
        public long CurrentUserId => User.GetUserId();
        public string CurrentUserName => User.GetUserName();
        public string CurrentPageUrl => HttpContext.Request.Path;
        //public IStockInventoryMangement _stockInventoryMangement => RestService.For<IStockInventoryMangement>(httpClient);



    }
}
