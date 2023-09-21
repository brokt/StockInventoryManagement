using Core.Entities;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nest;
using Refit;
using System.Security.Claims;
using Web.Infrastructure;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Web.Pages.Auth
{
    public class LoginModel : PageBaseModel
    {
		private readonly IStockInventoryMangement _stockInventoryMangement;

		public LoginModel(IConfiguration configuration,IStockInventoryMangement stockInventoryMangement)
        {
            _stockInventoryMangement = stockInventoryMangement;
        }

        [BindProperty]
        public LoginUserQuery Item { get; set; } = new LoginUserQuery();

        //public LoginModel(IStockInventoryMangement stockInventoryMangement)
        //{
        //	_stockInventoryMangement = stockInventoryMangement;
        //}
        //public LoginModel()
        //{
        //    //_stockInventoryMangement = RestService.For<IStockInventoryMangement>("https://localhost:5001", new RefitSettings()
        //    //{
        //    //	ExceptionFactory = httpResponse => Task.FromResult<Exception>(null),

        //    //});

        //    _stockInventoryMangement = RestService.For<IStockInventoryMangement>("https://localhost:5001");
        //}

        public void OnGet()
        {
        }

        public async Task<ApiResponse<T>> ExecAsync<T>(
    Func<Task<ApiResponse<T>>> method) where T : class
        {
            var apiResponseOfT = await method();
            if (apiResponseOfT.IsSuccessStatusCode)
            {
                //do other stuff
                return apiResponseOfT;
            }
            else
            {
                //do some logging etc..
                return apiResponseOfT;
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var response = await _stockInventoryMangement.Login("1", Item);
            if (response.IsSuccessStatusCode)
            {
                if (response.Content.Success)
                {
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, Item.Email));
                    identity.AddClaim(new Claim(ClaimTypes.Name, Item.Email));
                    if (response.Content?.Data.Claims != null)
                    {
                        foreach (var claim in response.Content.Data.Claims)
                        {
                            identity.AddClaim(new Claim(ClaimTypes.Role, claim));
                        }
                        foreach (var group in response.Content.Data.Groups)
                        {
                            identity.AddClaim(new Claim(group,"true"));
                        }

                        identity.AddClaim(new Claim("token", response.Content.Data.Token));
                        identity.AddClaim(new Claim("refreshToken", response.Content.Data.RefreshToken));
                    }
                        
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        principal,
                        new AuthenticationProperties
                        {
                            IsPersistent = true,
                            AllowRefresh = true,
                            ExpiresUtc = DateTime.UtcNow.AddYears(1)
                        });

                    //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    //_httpContextAccessor.HttpContext.Request.Headers.Add("Authorization", $"Bearer {response.Content.Data.Token}");
                    return RedirectToPage("/Index");
                }
            }
            return Page();
        }

        //private void SignIn(AccessTokenIDataResult user)
        //{
        //	var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
        //	//identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user?.Data.Id.ToString() ?? ""));
        //	//identity.AddClaim(new Claim(ClaimTypes.Name, user?.UserName ?? ""));
        //	//identity.AddClaim(new Claim(ClaimTypes.GivenName, $"{user?.FirstName} {user?.LastName}"));
        //	user.Data.
        //	if (user?.Data.Claims != null)
        //		foreach (var role in user.Data.Claims)
        //		{
        //			identity.AddClaim(new Claim(ClaimTypes.Role, role));
        //		}

        //	var principal = new ClaimsPrincipal(identity);

        //	HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        //}
    }
}
