using Core.Extensions;
using Core.Flash;
using Core.Utilities.Messages;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web.Infrastructure;

namespace Web.Pages.WareHouses
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
        public Warehouse Warehouse { get; set; }    
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
                    Warehouse = await Getbyid(id);

                    if (Warehouse == null)
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

        private async Task<Warehouse> Getbyid(int? id)
        {
            return await _stockInventoryMangement.GetWarehouseById(id).GetDataOrThrow();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                if (Warehouse.Id > 0)
                {

                    await _stockInventoryMangement.WarehousesPUT(Warehouse.Adapt<UpdateWarehouseCommand>()).GetDataOrThrow();
                    flasher.Success(FlashMessages.SuccessUpdate, true);
                }
                else
                {
                    await _stockInventoryMangement.WarehousesPOST(Warehouse.Adapt<CreateWarehouseCommand>()).GetDataOrThrow();

                    flasher.Success(FlashMessages.SuccessAdd, true);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await WarehouseExistsAsync(Warehouse.Id).ConfigureAwait(false))
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
            var id = Warehouse.Id;
            var item = await Getbyid(id);

            if (item == null)
            {
                return NotFound();
            }
            else
            {
                //item.IsDeleted = true;
                await _stockInventoryMangement.WarehousesDELETE(item.Adapt<DeleteWarehouseCommand>()).GetDataOrThrow();
                flasher.Success(FlashMessages.Success, true);
            }

            return RedirectToPage("./Index");
        }

        private async Task<bool> WarehouseExistsAsync(int id)
        {
            return (await Getbyid(id)).Id > 0;
        }
    }
}
