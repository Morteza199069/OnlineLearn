using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineLearn.Core.Security;
using OnlineLearn.Core.Services.Interfaces;

namespace OnlineLearn.Web.Pages.Admin.Discount
{
    [PermissionChecker(1)]
    public class IndexModel : PageModel
    {
        private IOrderService _orderService;

        public IndexModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public List<DataLayer.Entities.Order.Discount> Discounts { get; set; }

        public void OnGet()
        {
           Discounts = _orderService.GetAllDiscounts();
        }
    }
}
