using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearn.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearn.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class MyOrdersController : Controller
    {
        private IOrderService _orderService;

        public MyOrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShowOrder(int id, bool Finalized = false)
        {
            var order = _orderService.GetOrderForUserPanel(User.Identity.Name, id);
            if(order==null)
            {
                return NotFound();
            }

            ViewBag.Finalized = Finalized;
            return View(order);
        }

        public IActionResult FinalzeOrder(int id)
        {
            if (_orderService.FinalOrder(User.Identity.Name, id))
            {
                return Redirect("/UserPanel/MyOrders/" + id + "?Finalized=true");
            }

            return BadRequest();
        }
    }
}
