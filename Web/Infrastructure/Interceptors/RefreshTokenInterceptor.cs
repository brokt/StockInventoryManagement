using System.Net.Http.Headers;
using System.Net;
using Core.Extensions;

namespace Web.Infrastructure.Interceptors
{
	public class RefreshTokenInterceptor : DelegatingHandler
	{
		private readonly IStockInventoryMangement _stockInventoryMangement;
		private readonly IHttpContextAccessor httpContextAccessor;

		public RefreshTokenInterceptor(IStockInventoryMangement stockInventoryMangement, IHttpContextAccessor httpContextAccessor)
		{
			_stockInventoryMangement = stockInventoryMangement;
			this.httpContextAccessor = httpContextAccessor;
		}

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var response = await base.SendAsync(request, cancellationToken);

			if (response.StatusCode == HttpStatusCode.Forbidden)
			{
				var refreshToken = await _stockInventoryMangement.RefreshToken("1",new LoginWithRefreshTokenQuery() { RefreshToken = httpContextAccessor.HttpContext?.User.GetRefreshToken() }).GetDataOrThrow();

				request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", refreshToken.Data.Token);

				response = await base.SendAsync(request, cancellationToken);
			}

			return response;
		}
	}
}
