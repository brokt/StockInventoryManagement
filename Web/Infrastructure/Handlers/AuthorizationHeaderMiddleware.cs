using Core.Extensions;

namespace Web.Infrastructure.Handlers
{
	public class AuthorizationHeaderMiddleware
	{
		//private readonly RequestDelegate _next;
		private readonly IHttpContextAccessor httpContextAccessor;

		public AuthorizationHeaderMiddleware(IHttpContextAccessor httpContextAccessor)
		{
			this.httpContextAccessor = httpContextAccessor;
		}

		public async Task Invoke(HttpContext context, RequestDelegate next)
		{

			// Erişim token'ını istek başlığına ekle
			context.Request.Headers.Add("Authorization", $"Bearer {httpContextAccessor.HttpContext?.User.GetToken()}");

			await next(context);
		}
	}
}
