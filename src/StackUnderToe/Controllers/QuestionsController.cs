using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kennel.Helpers;
using Dapper;

namespace StackUnderToe.Controllers
{
    public class QuestionsController : Controller
    {
        public ActionResult Index()
        {
            using (var connection = ConnectionHelper.GetConnection())
            {
                var posts = connection.Query<Post>(@"
                                SELECT TOP (@Top) *
                                FROM Posts
                                WHERE PostTypeId = 1
                                    AND AcceptedAnswerId IS NULL
                                ORDER BY CreationDate DESC", new { Top = 20 });

                return View(posts);
            }
        }
    }
}
