using AutoMapper;
using BLL.AbstractServices;
using BLL.Dtos;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using MVCBlogSite.Models;
using System.Diagnostics;

namespace MVCBlogSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IPostService postService, IMapper mapper)
        {
            _logger = logger;
            _postService = postService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var role = HttpContext.Session.GetString("IsAdmin");
            
            if (role == "True")
            {
                var unApprovedPosts = await _postService.GettAllUnApprovedPosts();
                var allMappedPost = _mapper.Map<List<PostViewModel>>(unApprovedPosts);

                return View(allMappedPost);
            }

            var posts = await _postService.GetAllPosts();
            var mappedPost = _mapper.Map<List<PostViewModel>>(posts);

            return View(mappedPost);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostViewModel postViewModel)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Login", "Account");

            postViewModel.UserId = userId.Value;

            var mappedPost = _mapper.Map<PostDto>(postViewModel);
            await _postService.CreatePost(mappedPost);
            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int postId)
        {
            await _postService.DeletePost(postId);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Approve(int postId)
        {
            await _postService.ApprovePost(postId);

            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public async Task<IActionResult> GetApprovalPendingPosts()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var postDtos = await _postService.GettAllUnApprovedPosts();
            postDtos = postDtos.Where(x => x.UserId == userId && !x.IsApproved).ToList();
            var mappedPosts = _mapper.Map<List<PostViewModel>>(postDtos);
            
            return View(mappedPosts);
        }


        [HttpPost]
        public async Task<IActionResult> Like(int postId)
        {
            await _postService.LikePost(postId);

            return RedirectToAction("Index", "Home");
        }








        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
