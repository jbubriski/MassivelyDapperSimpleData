using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kennel.Helpers;
using Dapper;

namespace StackUnderToe.Controllers
{
    public class UsersController : Controller
    {
        public ActionResult Index()
        {
            using (var connection = ConnectionHelper.GetConnection())
            {
                var users = connection.Query<User>(@"
SELECT TOP (@Top) *
FROM Users
ORDER BY CreationDate DESC", new { Top = 20 });

                return View(users);
            }
        }

        public ActionResult Top()
        {
            using (var connection = ConnectionHelper.GetConnection())
            {
                var users = connection.Query<User>(@"
SELECT TOP (@Top) *
FROM Users
ORDER BY Reputation DESC", new { Top = 20 });

                return View(users);
            }
        }

        public ActionResult Oldest()
        {
            using (var connection = ConnectionHelper.GetConnection())
            {
                var users = connection.Query<User>(@"
SELECT TOP (@Top) *
FROM Users
ORDER BY CreationDate ASC", new { Top = 20 });

                return View(users);
            }
        }

        public ActionResult Details(int id)
        {
            var sql = @"
SELECT TOP 1 *
FROM Users
WHERE Id = @Id

SELECT TOP 5 *
FROM Posts
WHERE PostTypeId = 1
  AND OwnerUserId = @Id

SELECT TOP 5 *
FROM Posts
WHERE PostTypeId = 2
  AND OwnerUserId = @Id";

            using (var connection = ConnectionHelper.GetConnection())
            using (var queryResults = connection.QueryMultiple(sql, new { Id = id }))
            {
                ViewData.Model = queryResults.Read<User>().Single();

                ViewBag.Questions = queryResults.Read<Post>().ToList();
                ViewBag.Answers = queryResults.Read<Post>().ToList();
            };

            return View();
        }
    }
}
