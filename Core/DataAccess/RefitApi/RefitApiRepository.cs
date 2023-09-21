using Core.Extensions;
using Core.Utilities.Handlers;
using MailKit.Net.Imap;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Configuration;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.RefitApi
{
	public class RefitApiRepository : IRefitApiRepository
	{
		private readonly IConfiguration _configuration;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public RefitApiRepository(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
			_configuration = configuration;
			_httpContextAccessor = httpContextAccessor;
		}
        public T GetClientService<T>()
		{
			var httpClient = new HttpClient(new _AuthorizationHeaderMiddleware(_httpContextAccessor.HttpContext?.User.GetToken(), new HttpClientHandler()))
			{
				BaseAddress = new Uri(_configuration.GetSection("BaseUrl").Get<string>())
			};
			var apiClient = RestService.For<T>(httpClient);
			return apiClient;
		}
	}
}
