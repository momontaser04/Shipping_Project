using BL.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Shipping_Project.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        private readonly IShippingType _shippingTypeService;
        public HomeController(IShippingType shipping)
        {
            _shippingTypeService = shipping;

        }
        public async Task<IActionResult> Index()
        {
            var result= await _shippingTypeService.GetAllAsync();
            return View(result);
        }
    }
}
