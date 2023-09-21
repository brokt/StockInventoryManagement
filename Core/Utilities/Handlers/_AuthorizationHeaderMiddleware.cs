using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Utilities.Handlers
{
	public class _AuthorizationHeaderMiddleware : DelegatingHandler
	{
		private readonly string _token;

		public _AuthorizationHeaderMiddleware(string token, HttpClientHandler handler)
		{
			_token = token;
			base.InnerHandler = handler;
		}

		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
			return base.SendAsync(request, cancellationToken);
		}
	}
}
