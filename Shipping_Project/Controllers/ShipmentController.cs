using BL.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Shipping_Project.Controllers
{
    [Authorize]
    public class ShipmentController : Controller
    {
        private readonly ILogger<ShipmentController> _logger;
        private readonly IShipment _shipment;

        public ShipmentController(ILogger<ShipmentController> logger, IShipment shipment)
        {
            _logger = logger;
            _shipment = shipment;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> List(int page=1)
        {
            var shipments =await _shipment.GetShipments(page,5);

            return View(shipments);
        }
        public IActionResult Show(Guid id)
        {
            return View();
        }

        public  IActionResult Edit(Guid id)
        {

            return View();
        }

        public async Task<IActionResult> Delete(Guid id)
        {
           await _shipment.Changestatus(id, 0);
            return RedirectToAction("List");
        }
    }
}
