using Core.Extensions;
using Core.Flash;
using Core.Utilities.Messages;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json;
using Web.Infrastructure;

namespace Web.Pages.Orders
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
        public Order Order { get; set; }
        [BindProperty]
        public string ProductAddOnProductGroupsJson { get; set; } = "[]";
        public List<Product> ProductAddOnProductGroups => /*JsonSerializer.Deserialize<List<Product>>(ProductAddOnProductGroupsJson);*/ JsonConvert.DeserializeObject<List<Product>>(ProductAddOnProductGroupsJson,new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore});
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
                    Order = await Getbyid(id);

                    if (Order == null)
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

        private async Task<Order> Getbyid(int? id)
        {
            return await _stockInventoryMangement.Getbyid(id).GetDataOrThrow();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                List<OrderItem> orderItems = new List<OrderItem>();
                foreach (var item in ProductAddOnProductGroups)
                {
                    orderItems.Add(new OrderItem()
                    {
                        OrderId = Order.Id,
                        ProductId = item.Id,
                        Quantity = 0,
                        UnitPrice = item.UnitPrice,
                    });

                }
                Order.OrderItems = orderItems;
                if (Order.Id > 0)
                {

                    await _stockInventoryMangement.OrdersPUT(Order.Adapt<UpdateOrderCommand>()).GetDataOrThrow();
                    flasher.Success(FlashMessages.SuccessUpdate, true);
                }
                else
                {
                    await _stockInventoryMangement.OrdersPOST(Order.Adapt<CreateOrderCommand>()).GetDataOrThrow();

                    flasher.Success(FlashMessages.SuccessAdd, true);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await OrderExistsAsync(Order.Id).ConfigureAwait(false))
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
            var id = Order.Id;
            var item = await Getbyid(id);

            if (item == null)
            {
                return NotFound();
            }
            else
            {
                //item.IsDeleted = true;
                await _stockInventoryMangement.OrdersDELETE(item.Adapt<DeleteOrderCommand>()).GetDataOrThrow();
                flasher.Success(FlashMessages.Success, true);
            }

            return RedirectToPage("./Index");
        }

        public async Task<JsonResult> OnGetReadCustomers()
        {
            var result = await _stockInventoryMangement.GetCustomersList().GetDataOrThrow();
            return new JsonResult(result);
        }
        public async Task<JsonResult> OnPostReadProducts([DataSourceRequest]DataSourceRequest request)
        {
            var result = await _stockInventoryMangement.GetProductsList().GetDataOrThrow();
            return new JsonResult(result.ToDataSourceResult(request));
        }

        private async Task<bool> OrderExistsAsync(int id)
        {
            return (await Getbyid(id)).Id > 0;
        }
    }
}
