using AutoMapper;
using BLL.AbstractServices;
using BLL.Dtos;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCBlogSite.Models;
using System.Diagnostics;

namespace MVCBlogSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostService _postService;
        private readonly IPostLikeService _postLikeService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IPostService postService, IPostLikeService postLikeService, ICategoryService categoryService, IMapper mapper)
        {
            _logger = logger;
            _postService = postService;
            _postLikeService = postLikeService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var role = HttpContext.Session.GetString("IsAdmin");
            
            if (role == "True")
            {
                var unApprovedPosts = await _postService.GettAllUnApprovedPosts();
                var allMappedPost = _mapper.Map<List<PostViewModel>>(unApprovedPosts);

                var categoriesa = await _categoryService.GetAllCategories();
                var allCategoriesa = _mapper.Map<List<CategoryViewModel>>(categoriesa);
                ViewBag.AllCategories = allCategoriesa;

                return View(allMappedPost);
            }

            var posts = await _postService.GetAllPosts();
            var mappedPost = _mapper.Map<List<PostViewModel>>(posts);

            // User'ın beğendiği postları ViewBag olarak Index.cshtml'e gönderir.
            var userId = HttpContext.Session.GetInt32("UserId").Value;
            var userPostLikes = await _postLikeService.GetUserPostLikes(userId);

            var userPostLikesSelectList = userPostLikes.Select(like => new SelectListItem
            {
                Value = like.PostId.ToString()
            }).ToList();

            // ViewBag'e atanması
            ViewBag.UserPostLikes = userPostLikesSelectList;

            var categories = await _categoryService.GetAllCategories();
            var allCategories = _mapper.Map<List<CategoryViewModel>>(categories);
            ViewBag.AllCategories = allCategories;

            return View(mappedPost);
        }
       
        [HttpPost]
        public async Task<IActionResult> Index(List<int> categoryIds)
        {
            var allPosts = await _postService.GetPostByCategory(categoryIds);
            var newAllPosts = _mapper.Map<List<PostViewModel>>(allPosts);
            var categories = await _categoryService.GetAllCategories();
            var allCategories = _mapper.Map<List<CategoryViewModel>>(categories);
            ViewBag.AllCategories = allCategories;

            // User'ın beğendiği postları ViewBag olarak Index.cshtml'e gönderir.
            var userId = HttpContext.Session.GetInt32("UserId").Value;
            var userPostLikes = await _postLikeService.GetUserPostLikes(userId);

            var userPostLikesSelectList = userPostLikes.Select(like => new SelectListItem
            {
                Value = like.PostId.ToString()
            }).ToList();

            // ViewBag'e atanması
            ViewBag.UserPostLikes = userPostLikesSelectList;

            return View(newAllPosts);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllCategories();
            var mappedCategories = _mapper.Map<List<CategoryViewModel>>(categories);
            ViewBag.Categories = mappedCategories;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostViewModel postViewModel, List<int> categoryIds)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Login", "Account");

            postViewModel.UserId = userId.Value;

            var mappedPost = _mapper.Map<PostDto>(postViewModel);

            await _postService.CreatePost(mappedPost);

            var newPost = await _postService.GettAllUnApprovedPosts();
            var newestPost = newPost.OrderByDescending(x => x.Id).FirstOrDefault();

            foreach (var item in categoryIds)
            {
                await _postService.Add(new PostCategoryDto { CategoryId = item, PostId = newestPost.Id });
            }

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
            var role = HttpContext.Session.GetString("IsAdmin");

            var postDtos = await _postService.GettAllUnApprovedPosts();
            postDtos = postDtos.Where(x => x.UserId == userId && !x.IsApproved).ToList();

            var mappedPosts = _mapper.Map<List<PostViewModel>>(postDtos);

            return View(mappedPosts);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllApprovalPendingPosts()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            var postDtos = await _postService.GettAllUnApprovedPosts();
            postDtos = postDtos.Where(x => !x.IsApproved).ToList();

            var mappedPosts = _mapper.Map<List<PostViewModel>>(postDtos);

            return View(mappedPosts);
        }

        [HttpPost]
        public async Task<IActionResult> Like(int postId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            var postLikeDtos = await _postLikeService.GetAllPostLikes();
            var filteredPostLike = postLikeDtos.FirstOrDefault(x => x.UserId == userId && x.PostId == postId);

            if (filteredPostLike == null)
                await _postLikeService.LikePost(userId.Value, postId);
            else
                await _postLikeService.UnLikePost(userId.Value, postId);

            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> Complain(int postId)
        {
            var userId = HttpContext.Session.GetInt32("UserId").Value;

            if (await _postService.ComplainExists(userId, postId))
            {
                TempData["ErrorMessage"] = "You already reported this post";
                TempData["PostId"] = postId;
                return RedirectToAction("Index", "Home");
            }

            await _postService.ReportPost(userId, postId);
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public async Task<IActionResult> GetComplainPosts()
        {
            var complains = await _postService.GetComplainPosts();
            var mappedComplains = _mapper.Map<List<ComplainViewModel>>(complains);
            return View(mappedComplains);
        }



        [HttpGet]
        public async Task<IActionResult> Categories()
        {
            var role = HttpContext.Session.GetString("IsAdmin");
            if (role == "True")
            {
                var categories = await _categoryService.GetAllCategories();
                var mappedCategories = _mapper.Map<List<CategoryViewModel>>(categories);

                return View(mappedCategories);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                var mappedCategory = _mapper.Map<CategoryDto>(categoryViewModel);

                await _categoryService.AddCategory(mappedCategory);
            }

            return RedirectToAction("Categories");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
