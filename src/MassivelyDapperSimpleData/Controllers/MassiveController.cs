using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MassivelyDapperSimpleData.DataAccessMassive;

namespace MassivelyDapperSimpleData.Controllers
{
    public class MassiveController : Controller
    {
        public ActionResult Index()
        {
            var posts = new Posts();

            return View(posts.All(orderBy: "DateCreated DESC", limit: 20));
        }

        public ActionResult PostsByCategory(string categoryName)
        {
            var posts = new Posts();

            return View(posts.All(where: "WHERE categoryName = @0", orderBy: "DateCreated DESC", args: categoryName));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            var posts = new Posts();

            var post = Request.Form.ToExpando();

            post.DateCreated = DateTime.Now;
            post.DateModified = DateTime.Now;

            posts.Insert(post);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var posts = new Posts();

            var post = posts.Single(id);

            if (post == null)
            {
                return RedirectToAction("Index");
            }

            ViewData.Model = post;

            return View();
        }

        [HttpPost]
        public ActionResult Update(int id, FormCollection form)
        {
            var posts = new Posts();

            posts.Update(Request.Form, id);

            return RedirectToAction("Index");
        }
    }
}
