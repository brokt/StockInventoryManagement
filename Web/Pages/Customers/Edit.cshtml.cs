using Core.Extensions;
using Core.Flash;
using Core.Utilities.Messages;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web.Infrastructure;

namespace Web.Pages.Customers
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
        public Customer Customer { get; set; }
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
                    Customer = await Getbyid(id);

                    if (Customer == null)
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

        private async Task<Customer> Getbyid(int? id)
        {
            return await _stockInventoryMangement.GetCustomerById(id).GetDataOrThrow();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                if (Customer.Id > 0)
                {

                    await _stockInventoryMangement.UpdateCustomer(Customer.Adapt<UpdateCustomerCommand>()).GetDataOrThrow();
                    flasher.Success(FlashMessages.SuccessUpdate, true);
                }
                else
                {
                    await _stockInventoryMangement.AddCustomer(Customer.Adapt<CreateCustomerCommand>()).GetDataOrThrow();

                    flasher.Success(FlashMessages.SuccessAdd, true);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CustomerExistsAsync(Customer.Id).ConfigureAwait(false))
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
            var id = Customer.Id;
            var item = await Getbyid(id);

            if (item == null)
            {
                return NotFound();
            }
            else
            {
                //item.IsDeleted = true;
                await _stockInventoryMangement.DeleteCustomer(item.Adapt<DeleteCustomerCommand>()).GetDataOrThrow();
                flasher.Success(FlashMessages.Success, true);
            }

            return RedirectToPage("./Index");
        }

        private async Task<bool> CustomerExistsAsync(int id)
        {
            return (await Getbyid(id)).Id > 0;
        }
    }
}
