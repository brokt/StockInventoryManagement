using Core.Extensions;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Infrastructure;

namespace Web.Pages.Commerce.Categories
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
            var result = await _stockInventoryMangement.GetCategoriesList().GetDataOrThrow();
            return new JsonResult(result.ToDataSourceResult(request));
        }
        public async Task<JsonResult> OnPostCreate(CreateCategoryCommand category)
        {
            var result = await _stockInventoryMangement.AddCategory(category).GetDataOrThrow();
            return new JsonResult(result);
        }
        
        public async Task<JsonResult> OnPostUpdate(UpdateCategoryCommand updateCategoryCommand)
        {
            var result = await _stockInventoryMangement.UpdateCategory(updateCategoryCommand).GetDataOrThrow();
            return new JsonResult(result);
        }
        
        public async Task<JsonResult> OnPostDelete(DeleteCategoryCommand deleteCategoryCommand)
        {
            var result = await _stockInventoryMangement.DeleteCategory(deleteCategoryCommand).GetDataOrThrow();
            return new JsonResult(result);
        }
    }
}
