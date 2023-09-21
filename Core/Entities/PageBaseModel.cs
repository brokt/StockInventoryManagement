using Core.Extensions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class PageBaseModel : PageModel
    {
        public long CurrentUserId => User.GetUserId();
        public string CurrentUserName => User.GetUserName();
        public string CurrentPageUrl => HttpContext.Request.Path;
        
    }
}
