using Core.Flash;
using Core.Utilities.Messages;
using Core.Extensions;
using Kendo.Mvc.UI;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web.Infrastructure;
using Kendo.Mvc.Extensions;

namespace Web.Pages.Commerce.Products
{
    public class EditModel : PageBaseModel
    {
        private readonly IStockInventoryMangement _stockInventoryMangement;
        private readonly IFlasher flasher;
        public EditModel(IStockInventoryMangement stockInventoryMangement, IFlasher flasher)
        {
            _stockInventoryMangement = stockInventoryMangement;
            this.flasher = flasher;
        }
        [BindProperty]
        public Product Product { get; set; }  
        [BindProperty]
        public List<string> ListProductCategories { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id.HasValue)
            {
                if (id == 0)
                {

                    return Page();
                }
                else if (id > 0)
                {
                    Product = await Getbyid(id);

                    if (Product == null)
                    {
                        return NotFound();
                    }
                    else
                    {

                        return Page();
                    }
                }
            }

            return RedirectToPage("./Index");
        }

        private async Task<Product> Getbyid(int? id)
        {
            return await _stockInventoryMangement.GetProductById(id).GetDataOrThrow();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                if (Product.Id > 0)
                {

                    await _stockInventoryMangement.ProductsPUT(Product.Adapt<UpdateProductCommand>()).GetDataOrThrow();
                    flasher.Success(FlashMessages.SuccessUpdate, true);
                }
                else
                {
                    await _stockInventoryMangement.ProductsPOST(Product.Adapt<CreateProductCommand>()).GetDataOrThrow();

                    flasher.Success(FlashMessages.SuccessAdd, true);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ProductExistsAsync(Product.Id).ConfigureAwait(false))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            var id = Product.Id;
            var item = await Getbyid(id);

            if (item == null)
            {
                return NotFound();
            }
            else
            {
                //item.IsDeleted = true;
                await _stockInventoryMangement.ProductsDELETE(item.Adapt<DeleteProductCommand>()).GetDataOrThrow();
                flasher.Success(FlashMessages.Success, true);
            }

            return RedirectToPage("./Index");
        }

        private async Task<bool> ProductExistsAsync(int id)
        {
            return (await Getbyid(id)).Id > 0;
        }

        public async Task<JsonResult> OnGetReadCategories(int? Id)
        {
            var result = await _stockInventoryMangement.GetHierarchicalCategoriesList(Id).GetDataOrThrow();
            //var result = await _refitApiRepository.GetClientService<IStockInventoryMangement>().Getcategorieslist();
            var ss = result.Select(x => new
            {
                id = x.Id,
                name = x.Name,
                hasChildren = x.HasChildren
            });
            return new JsonResult(ss);
        }
    }
}
