using BL.Contract;
using BL.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shipping_Project.Models;
using Shipping_Project.Services;
using System.Threading.Tasks;

namespace Shipping_Project.Controllers
{
    public class AccountController : Controller
    {
        IUserService _userService;
        private readonly GenericApiClient _apiClient;
        public AccountController(IUserService userService, GenericApiClient apiClient)
        {
            _userService = userService;
            _apiClient = apiClient;
        }
    
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserDto user)
        {
            if (!ModelState.IsValid)
                return View(user);
            var result = await _userService.RegisterAsync(user);
            if (result.Success)
            {
                return RedirectToRoute(new { controller = "Account", action = "Login" });
            }
            else
                return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
           await _userService.LogoutAsync();
            return View("Login");
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto user)
        {
            var result = await _userService.LoginAsync(user);
            if (result.Success)
            {
                LoginApiModel apiResult = await _apiClient.PostAsync<LoginApiModel>("api/auth/login", user);

                if (apiResult == null)
                {
                    ModelState.AddModelError(string.Empty, "API error: Unable to process login.");
                    return View(user);
                }

                var accessToken = apiResult?.AccessToken.ToString();

                if (string.IsNullOrEmpty(accessToken))
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(user);
                }
             
                Response.Cookies.Append("AccessToken", accessToken, new CookieOptions
                {
                    HttpOnly = false,
                    Secure = true,
                    Expires = DateTime.UtcNow.AddMinutes(15)  
                });
                Response.Cookies.Append("RefreshToken", apiResult?.RefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTime.UtcNow.AddDays(7)  // Adjust token expiry based on your needs
                });

                var dbuser = await _userService.GetUserByEmailAsync(user.Email);

                if(dbuser.Role.ToLower() == "admin")
                    return RedirectToRoute(new { area = "Admin", controller = "Home", action = "Index" });
                else

                    return RedirectToRoute(new {  controller = "Home", action = "Index" });
            }
            else
                return View();
        }
    }
}
