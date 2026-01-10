using BL.Contract;
using BL.Dtos;
using Domains.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shipping_Project.Helpers;
using System.Threading.Tasks;

namespace Shipping_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ShippingTypesController : Controller
    {
        private readonly IShippingType _shippingTypeService;
        public ShippingTypesController(IShippingType shippingTypeService)
        {
            _shippingTypeService = shippingTypeService;
        }
     
        public async Task<IActionResult> List()
        {
            var result = await _shippingTypeService.GetAllAsync();
            return View(result);
        }

        public async Task<IActionResult> Edit(Guid? ID)
        {
            var data = new ShippingTypeDto();

            if(ID != null)
            data =await _shippingTypeService.GetByIdAsync((Guid)ID);
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(ShippingTypeDto data)
        {
            TempData["MessageType"] = null;
            if (!ModelState.IsValid)
                return View("Edit", data);
            try
            {
                if (data.Id == Guid.Empty)
                 await   _shippingTypeService.AddAsync(data);
                else
                 await   _shippingTypeService.UpdateAsync(data);
                TempData["MessageType"] = MessageTypes.SaveSucess;
            }
            catch (Exception ex)
            {
                TempData["MessageType"] = MessageTypes.SaveFailed;
            }

            return RedirectToAction("List");
        }

        public async Task<IActionResult> Delete(Guid Id)
        {
            TempData["MessageType"] = null;
            try
            {
              await  _shippingTypeService.Changestatus(Id,0);
                TempData["MessageType"] = MessageTypes.DeleteSuccess;
            }
            catch (Exception ex)
            {
                TempData["MessageType"] = MessageTypes.DeleteFailed;
            }

            return RedirectToAction("List");
        }
    }
}
