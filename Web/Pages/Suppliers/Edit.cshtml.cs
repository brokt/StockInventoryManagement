using Core.Extensions;
using Core.Flash;
using Core.Utilities.Messages;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web.Infrastructure;

namespace Web.Pages.Suppliers
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
        public Supplier Supplier { get; set; }
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
                    Supplier = await Getbyid(id);

                    if (Supplier == null)
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

        private async Task<Supplier> Getbyid(int? id)
        {
            return await _stockInventoryMangement.GetSupplierById(id).GetDataOrThrow();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                if (Supplier.Id > 0)
                {

                    await _stockInventoryMangement.SuppliersPUT(Supplier.Adapt<UpdateSupplierCommand>()).GetDataOrThrow();
                    flasher.Success(FlashMessages.SuccessUpdate, true);
                }
                else
                {
                    await _stockInventoryMangement.SuppliersPOST(Supplier.Adapt<CreateSupplierCommand>()).GetDataOrThrow();

                    flasher.Success(FlashMessages.SuccessAdd, true);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await SupplierExistsAsync(Supplier.Id).ConfigureAwait(false))
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
            var id = Supplier.Id;
            var item = await Getbyid(id);

            if (item == null)
            {
                return NotFound();
            }
            else
            {
                //item.IsDeleted = true;
                await _stockInventoryMangement.SuppliersDELETE(item.Adapt<DeleteSupplierCommand>()).GetDataOrThrow();
                flasher.Success(FlashMessages.Success, true);
            }

            return RedirectToPage("./Index");
        }

        private async Task<bool> SupplierExistsAsync(int id)
        {
            return (await Getbyid(id)).Id > 0;
        }
    }
}
