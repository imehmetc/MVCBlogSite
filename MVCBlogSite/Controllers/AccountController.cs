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
                var users = await _userService.GetAllUsers();
                var existingUser = users.FirstOrDefault(x => x.UserName == userViewModel.UserName);
                if (existingUser != null)
                {
                    ViewBag.ErrorMessage = "Username already exists.";
                    return View(userViewModel);
                }
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
            else
                ViewBag.ErrorMessage = "Invalid username or password.";

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Account");
        }

        public async Task<IActionResult> Profile()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            var users = await _userService.GetAllUsers();

            var user = users.FirstOrDefault(x => x.Id == userId && x.IsAdmin == false);
            
            if (user == null)
                return RedirectToAction("Index", "Home");
            
            var mappedUser = _mapper.Map<UserViewModel>(user);
            
            return View(mappedUser);
        }

    }
}
