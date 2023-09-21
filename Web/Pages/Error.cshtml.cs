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

            // Hatan�n t�r�ne g�re i�lemler yapabilirsiniz
            if (exceptionHandlerPathFeature?.Error is RefitApiException)
            {
                // �zel bir hata i�leme kodu
                // ...
            }


            if (id == "400")
            {
                Title = "Hatal� �stek";
                Description = "Talebiniz s�ras�nda bir hata olu�tu";
            }
            else if (id == "401")
            {
                Title = "Yetkisiz Eri�im";
                Description = "Eri�meye �al��t���n�z sayfa do�rulama gerektirmektedir";
            }
            else if (id == "403")
            {
                Title = "Eri�im Yok";
                Description = "Belirtilen sayfaya eri�iminiz bulunmamaktad�r.";
            }
            else
            {
                Title = "Sayfa Bulunamad�";
                Description = "Arad���n�z sayfa ta��nm��, kald�r�lm��, yeniden adland�r�lm�� veya hi� yay�nlanmam�� olabilir.";
            }
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            await Task.CompletedTask;

            return Page();
        }
    }
}