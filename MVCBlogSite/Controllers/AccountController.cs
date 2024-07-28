using AutoMapper;
using BLL.AbstractServices;
using BLL.Dtos;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            if (userViewModel.Photo != null)
            {
                var fileName = Path.GetFileName(userViewModel.Photo.FileName);
                var filePath = Path.Combine("wwwroot", "img", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await userViewModel.Photo.CopyToAsync(stream);
                }

                userViewModel.PhotoUrl = fileName;
            }

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
            {
                HttpContext.Session.SetInt32("UserId", 0);
                ViewBag.ErrorMessage = "Invalid username or password.";
            }

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Account");
        }

        public async Task<IActionResult> Profile(int id)
        {
            var users = await _userService.GetAllUsers();

            UserDto user;

            if (id != 0)
                user = users.FirstOrDefault(x => x.Id == id && x.IsAdmin == false);
            else
                user = users.FirstOrDefault(x => x.Id == HttpContext.Session.GetInt32("UserId") && x.IsAdmin == false);


            if (user == null)
                return RedirectToAction("Index", "Home");

            var mappedUser = _mapper.Map<UserViewModel>(user);

            // user posts
            int userId;
            if (id != 0)
                userId = id;
            else
                userId = HttpContext.Session.GetInt32("UserId").Value;

            var userPosts = await _userService.GetAllPostsByUserId(userId);
            userPosts =  userPosts.Where(x => x.IsDeleted == false).ToList();
            var mappedPosts = _mapper.Map<List<PostViewModel>>(userPosts);
            ViewBag.UserPosts = mappedPosts;

            return View(mappedUser);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePhoto(int userId, IFormFile photo)
        {
            var user = await _userService.GetUserById(userId);
            var userViewModel = _mapper.Map<UserViewModel>(user);
            userViewModel.Photo = photo;

            var fileName = Path.GetFileName(userViewModel.Photo.FileName);
            var filePath = Path.Combine("wwwroot", "img", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await userViewModel.Photo.CopyToAsync(stream);
            }

            userViewModel.PhotoUrl = fileName;

            await _userService.Update(_mapper.Map<UserDto>(userViewModel));

            return RedirectToAction("Index", "Home");
        }

        public async void DeleteImg(UserViewModel userViewModel)
        {
            // Bu resmi kullanan başka ürün var mı? (kendisi dışında)
            var users = await _userService.GetAllUsers();
            bool otherUsersExist = users.Any(x => x.PhotoUrl == userViewModel.PhotoUrl && x.Id != userViewModel.Id);

            if (userViewModel.PhotoUrl != null && !otherUsersExist)
            {
                // O resmi klasörden sil

                // O resmin tam adını (patikasıyla beraber) bulalım.
                var file = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", userViewModel.PhotoUrl);

                // Silme metoduna bu patikayı gönder
                System.IO.File.Delete(file);
            }
        }
    }
}
