using Core.Extensions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Refit;
using System.Diagnostics;

namespace Web.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string Title { get; private set; }
        public string Description { get; private set; }

        private readonly ILogger<ErrorModel> _logger;

        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            // Hatanýn türüne göre iþlemler yapabilirsiniz
            if (exceptionHandlerPathFeature?.Error is RefitApiException)
            {
                // Özel bir hata iþleme kodu
                // ...
            }


            if (id == "400")
            {
                Title = "Hatalý Ýstek";
                Description = "Talebiniz sýrasýnda bir hata oluþtu";
            }
            else if (id == "401")
            {
                Title = "Yetkisiz Eriþim";
                Description = "Eriþmeye çalýþtýðýnýz sayfa doðrulama gerektirmektedir";
            }
            else if (id == "403")
            {
                Title = "Eriþim Yok";
                Description = "Belirtilen sayfaya eriþiminiz bulunmamaktadýr.";
            }
            else
            {
                Title = "Sayfa Bulunamadý";
                Description = "Aradýðýnýz sayfa taþýnmýþ, kaldýrýlmýþ, yeniden adlandýrýlmýþ veya hiç yayýnlanmamýþ olabilir.";
            }
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            await Task.CompletedTask;

            return Page();
        }
    }
}