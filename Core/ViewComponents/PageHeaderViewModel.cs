using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewComponents
{
    public class ButtonOptions
    {
        public string Text { get; set; }
        public string Link { get; set; }
        public string Method { get; set; }
        public string Color { get; set; }
    }

    public class PageHeaderViewModel
    {
        public string Title { get; set; }
        public string BackToListLink { get; set; }
        public string SaveMethod { get; set; }
        public string SaveAndNotifyMethod { get; set; }
        public string DeleteMethod { get; set; }
        public string AddNewLink { get; set; }
        public string AddNewText { get; set; }
        public string SearchFields { get; set; }
        public long? EditItemId { get; set; }
        public string SecondaryButtonText { get; set; }
        public string SecondaryButtonLink { get; set; }
        public ButtonOptions CustomButtonOptions { get; set; }
    }

    public class PageHeaderViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(PageHeaderViewModel model)
        {
            return Task.FromResult<IViewComponentResult>(View(model));
        }
    }

}
