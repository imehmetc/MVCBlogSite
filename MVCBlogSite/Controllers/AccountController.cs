using AutoMapper;
using BLL.AbstractServices;
using BLL.Dtos;
using Microsoft.AspNetCore.Mvc;
using MVCBlogSite.Models;

namespace MVCBlogSite.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AccountController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                var userDto = _mapper.Map<UserDto>(userViewModel);
                await _userService.Register(userDto);
                return RedirectToAction("Login");
            }

            return View(userViewModel);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password)
        {
            var userDto = await _userService.Login(userName, password);
            if (userDto != null)
            {
                var userViewModel = _mapper.Map<UserViewModel>(userDto);

                HttpContext.Session.SetInt32("UserId", userViewModel.Id);
                HttpContext.Session.SetString("UserName", userViewModel.UserName);
                HttpContext.Session.SetString("IsAdmin", userViewModel.IsAdmin.ToString());

                return RedirectToAction("Index", "Home", userViewModel);
            }

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Account");
        }

    }
}
