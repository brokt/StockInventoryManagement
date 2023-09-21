using Core.Extensions;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Infrastructure;

namespace Web.Pages.Suppliers
{
    public class IndexModel : PageBaseModel
    {
        private readonly IStockInventoryMangement _stockInventoryMangement;

        public IndexModel(IStockInventoryMangement stockInventoryMangement)
        {
            _stockInventoryMangement = stockInventoryMangement;
        }
        public void OnGet()
        {
        }

        public async Task<JsonResult> OnPostRead([DataSourceRequest] DataSourceRequest request)
        {
            var result = await _stockInventoryMangement.GetSuppliersList().GetDataOrThrow();
            return new JsonResult(result.ToDataSourceResult(request));
        }
    }
}
