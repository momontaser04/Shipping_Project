using BL.Contract;
using BL.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shipping_Project.Helpers;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Shipping_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CitiesController : Controller
    {
        private readonly ICities _ICity;
        private readonly ICountries _ICountry;
        public CitiesController(ICities ICity, ICountries iCountry)
        {
            _ICity = ICity;
            _ICountry = iCountry;
        }
        public IActionResult List()
        {
            var data = _ICity.GetAllCities();
            return View(data);
        }

        public async Task<IActionResult> Edit(Guid? Id)
        {
            TempData["MessageType"] = null;
            await LoadCountries();

            var data = new CityDto();
            if (Id != null)
            {
                data = await _ICity.GetByIdAsync((Guid)Id) ?? new CityDto();
            }

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(CityDto data)
        {
            TempData["MessageType"] = null;
            if (!ModelState.IsValid)
            {
                await LoadCountries();
                return View("Edit", data);
            }

            try
            {
                if (data.Id == Guid.Empty)
                    await _ICity.AddAsync(data);      // ✅
                else
                    await _ICity.UpdateAsync(data);   // ✅

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
               await _ICity.Changestatus(Id,0);
                TempData["MessageType"] = MessageTypes.DeleteSuccess;
            }
            catch (Exception ex)
            {
                TempData["MessageType"] = MessageTypes.DeleteFailed;
            }

            return RedirectToAction("List");
        }

        private async Task LoadCountries()
        {
            var countries = await _ICountry.GetAllAsync();
            ViewBag.Countries = countries ?? new List<CountryDto>();
        }

    }
}
