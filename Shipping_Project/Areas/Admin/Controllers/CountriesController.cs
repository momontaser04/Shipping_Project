using BL.Contract;
using BL.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shipping_Project.Helpers;

namespace Shipping_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CountriesController : Controller
    {
    private readonly ICountries _countries;
        public CountriesController(ICountries countries)
        {
            _countries = countries;
        }
        public async Task<IActionResult> List()
        {
            var result = await _countries.GetAllAsync();
            return View(result);
        }
        public async Task<IActionResult> Edit(Guid? userId)
        {
            var data = new CountryDto();
            if (userId != null)
                data = await _countries.GetByIdAsync((Guid)userId);
        

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(CountryDto data)
        {
            TempData["MessageType"] = null;
            if (!ModelState.IsValid)
                return View("Edit", data);
            try
            {
                if (data.Id == Guid.Empty)
                    await _countries.AddAsync(data);
                else
                    await _countries.UpdateAsync(data);
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
                await _countries.Changestatus(Id);
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
