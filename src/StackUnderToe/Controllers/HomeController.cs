using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Simple.Data;

namespace StackUnderToe.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var db = Database.OpenNamedConnection("Default");

            // TODO: refactor to use List<Post>
            var posts = db.Posts
                            .FindAll( db.Posts.AcceptedAnswerId == null && db.Posts.PostTypeId == 1 )
                            .OrderByDescending( db.Posts.ViewCount )
                            .ThenByDescending( db.Posts.CreationDate )
                            .Take(10);

            return View(posts);
        }
    }
}
