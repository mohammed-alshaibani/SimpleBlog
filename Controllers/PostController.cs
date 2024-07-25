using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SimpleBlog.Hubs;
using SimpleBlog.Models;
using SimpleBlog.Repo;

namespace SimpleBlog.Controllers
{
    public class PostController : Controller
    {
        private readonly IHubContext<NotificationHub> _notificationHubContext;
        private readonly IRepositiry<Post> repository;

        public PostController(IRepositiry<Post> repository, IHubContext<NotificationHub> notificationHubContext)
        {
            this.repository = repository;
            _notificationHubContext = notificationHubContext;
        }

        // GET: PostController
        public ActionResult Index()
        {
            var posts = repository.GetAll().ToList();
            ViewData["Post"] = posts;
            return View(posts);

        }

        // GET: PostController/Details/5
        public ActionResult Details(int id)
        {
            var Products = repository.GetById(id);
            if(Products == null || id == null)
            {
                return View("Not Found");
            }
            ViewData["Products"] = Products;
            return View(Products);
        }

        // GET: PostController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PostController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Post post)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(post);
                }
                else
                {
                    repository.Add(post);

                    // Send a notification to all clients
                    await _notificationHubContext.Clients.All.SendAsync("ReceivePostNotification", post.Title);

                    return RedirectToAction("Index", "Post");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"=====Exp In Edit Dept Action");
                Console.WriteLine($"{ex.Message}");
                TempData["msg"] = "خطأ غير متوقع";
                return View(post);
            }
        }

        // GET: PostController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                if (id == 0)
                {
                    TempData["msg"] = "يجب ادخال رقم  صحيح";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    var s = repository.GetById(id);
                    if (s == null)
                    {
                        TempData["msg"] = "يجب ادخال رقم طالب صحيح";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return View(s);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"=====Exp In Edit Student Action");
                Console.WriteLine($"{ex.Message}");
                TempData["msg"] = "خطأ غير متوقع";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: PostController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var res = repository.Update(post);
                TempData["msg"] = "تم التعديل بنجاح";
                return RedirectToAction("Index");
            }
        }

        // POST: PostController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int Id)
        {
            try
            {
                    var s = repository.GetById(Id);
                    if (s == null)
                    {
                        TempData["msg"] = "يجب ادخال رقم صحيح";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        repository.Delete(Id);
                        return RedirectToAction(nameof(Index));
                    }
             }
            catch (Exception ex)
            {
                Console.WriteLine($"=====Exp In Delete Student Action");
                Console.WriteLine($"{ex.Message}");
                TempData["msg"] = "خطأ غير متوقع";
                return RedirectToAction(nameof(Index));
            }
        }

    }


}
