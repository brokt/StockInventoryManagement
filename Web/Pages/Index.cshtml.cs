using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Infrastructure;

namespace Web.Pages
{
    [Authorize(Policy = "PanelRights")]
    public class IndexModel : PageBaseModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IStockInventoryMangement _stockInventoryMangement;
        public IndexModel(ILogger<IndexModel> logger, IStockInventoryMangement stockInventoryMangement)
        {
            _logger = logger;
            _stockInventoryMangement = stockInventoryMangement;
        }

        public void OnGet()
        {

        }
    }
}