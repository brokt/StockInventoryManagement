using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using Core.Extensions;
using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Core.Utilities.IoC;
using Refit;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http;

namespace Web.Infrastructure.Handlers
{
	public class AuthHeaderHandler : DelegatingHandler
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private bool _isRefreshed;

		public AuthHeaderHandler(IHttpContextAccessor httpContextAccessor)
		{
			//InnerHandler = new HttpClientHandler();
			this._httpContextAccessor = httpContextAccessor;
			_isRefreshed = false;
		}

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			//potentially refresh token here if it has expired etc.
			UpdateRequestAuthorizationHeader(request, _httpContextAccessor.HttpContext?.User.GetToken());
			/*request.Headers.Add("X-Tenant-Id", tenantProvider.GetTenantId())*/
			var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
			if (response.StatusCode == HttpStatusCode.Unauthorized /*&&!_isRefreshed*/)
			{
				response = await RefreshTokenAndRetryRequest(request, cancellationToken);
			}
			return response;
		}

		private async Task<HttpResponseMessage> RefreshTokenAndRetryRequest(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var refreshToken = _httpContextAccessor.HttpContext?.User.GetRefreshToken();
			var refreshedToken = await GetRefreshedToken(refreshToken);

			if (refreshedToken.Success)
			{
				UpdateRequestAuthorizationHeader(request, refreshedToken.Data.Token);
				UpdateRefreshTokenClaim(refreshedToken.Data.RefreshToken);
				UpdateTokenClaim(refreshedToken.Data.Token);
				UpdateHttpContextUser();

				//_isRefreshed = true;
				var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
				return response;
			}
			else
			{
				throw new Exception("Refresh token failed to retrieve a new token.");
			}
		}

		private async Task<AccessTokenIDataResult> GetRefreshedToken(string refreshToken)
		{
			var client = RestService.For<IStockInventoryMangement>(new HttpClient() { BaseAddress = new Uri("https://localhost:5001") });
			return (await client.RefreshToken("1", new LoginWithRefreshTokenQuery() { RefreshToken = refreshToken })).Content;
		}

		private void UpdateRequestAuthorizationHeader(HttpRequestMessage request, string token)
		{
			request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
		}

		private void UpdateRefreshTokenClaim(string refreshToken)
		{
			var refreshTokenClaim = new Claim("refreshToken", refreshToken);
			UpdateUserClaim(refreshTokenClaim);
		}

		private void UpdateTokenClaim(string token)
		{
			var tokenClaim = new Claim("token", token);
			UpdateUserClaim(tokenClaim);
		}

		private void UpdateUserClaim(Claim claim)
		{
			var identity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
			var existingClaim = identity?.FindFirst(claim.Type);
			if (existingClaim != null)
			{
				identity?.RemoveClaim(existingClaim);
			}
			identity?.AddClaim(claim);
		}

		private void UpdateHttpContextUser()
		{
			var updatedClaimsPrincipal = new ClaimsPrincipal(_httpContextAccessor.HttpContext.User.Identity);
			var authenticationProperties = new AuthenticationProperties
			{
				IsPersistent = true,
				AllowRefresh = true,
				ExpiresUtc = DateTime.UtcNow.AddDays(1)
			};
			_httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, updatedClaimsPrincipal, authenticationProperties).GetAwaiter().GetResult();
			_httpContextAccessor.HttpContext.User = updatedClaimsPrincipal;
		}
	}
}
