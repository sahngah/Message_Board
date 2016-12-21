using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using userDashboard.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace userDashboard.Controllers
{
    public class DashboardController : Controller
    {
        private DataContext _context;
        public DashboardController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("dashboard")]
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("curUserEmail") != null)
            {
                User RetrievedUser = _context.Users.SingleOrDefault(user => user.email == HttpContext.Session.GetString("curUserEmail"));
                ViewBag.curUser = RetrievedUser;
                List<Post> Posts = _context.Posts.Include(post => post.user).ToList();
                List<Comment> Comments = _context.Comments.Include(comment => comment.post).Include(comment => comment.user).ToList();
                ViewBag.Posts = Posts.OrderByDescending(x=> x.createdat);
                ViewBag.Comments = Comments;
                if(TempData["error"] != null)
                {
                    ViewBag.error = TempData["error"];
                    ViewBag.isThere = true;
                }else{
                    ViewBag.error = "";
                    ViewBag.isThere = false;
                }
                return View("dashboard");
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [Route("post")]
        public IActionResult Post(Post postpost)
        {
            Console.WriteLine("*********************************");
            if(ModelState.IsValid)
            {
                User thisUser = _context.Users.SingleOrDefault(user => user.email == HttpContext.Session.GetString("curUserEmail"));
                Post newPost = new Post
                {
                    userid = thisUser.id,
                    content = postpost.content,
                    createdat = DateTime.Now,
                    updatedat = DateTime.Now
                };
                _context.Posts.Add(newPost);
                _context.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            User RetrievedUser = _context.Users.SingleOrDefault(user => user.email == HttpContext.Session.GetString("curUserEmail"));
            ViewBag.curUser = RetrievedUser;
            return View("dashboard");
        }
        [HttpPost]
        [Route("comment")]
        public IActionResult comment(Comment PostComment)
        {
            if(PostComment.commentcontent == null)
            {
                TempData["error"] = "Comment section is required!";
                return RedirectToAction("dashboard");
            }
            User thisUser = _context.Users.SingleOrDefault(user => user.email == HttpContext.Session.GetString("curUserEmail"));
            Post thisPost = _context.Posts.SingleOrDefault(post => post.id == PostComment.id);
            Comment newComment = new Comment
            {
                postid = thisPost.id,
                userid = thisUser.id,
                commentcontent = PostComment.commentcontent,
                createdat = DateTime.Now,
                updatedat = DateTime.Now
            };
            _context.Comments.Add(newComment);
            _context.SaveChanges();
            return RedirectToAction("dashboard");
        }
        [HttpPost]
        [Route("delete")]
        public IActionResult delete(int id)
        {
            Console.WriteLine("*******************************************");
            Console.WriteLine(id);
            Post thisPost = _context.Posts.SingleOrDefault(post => post.id == id);
            _context.Posts.Remove(thisPost);
            _context.SaveChanges();
            return RedirectToAction("dashboard");
        }
    }
}
