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

        public ActionResult Oldest()
        {
            // TODO: showcase code snippets
            using (var connection = ConnectionHelper.GetConnection())
            {
                var users = connection.Query<User>(@"
SELECT TOP (@Top) *
FROM Users
ORDER BY CreationDate ASC", new { Top = 20 });

                return View(users);
            }
        }

        public ActionResult Top()
        {
            // TODO: Write this
            using (var connection = ConnectionHelper.GetConnection())
            {
                var users = connection.Query<User>(@"
SELECT TOP (@Top) *
FROM Users
ORDER BY Reputation DESC", new { Top = 20 });

                return View(users);
            }
        }

        public ActionResult Details(int id)
        {
            var sql = @"
SELECT *
FROM Users u
Join Badges b ON b.UserId = u.Id
WHERE u.Id = @Id

SELECT TOP 5 *
FROM Posts
WHERE PostTypeId = 1
  AND OwnerUserId = @Id
ORDER BY CreationDate DESC

SELECT TOP 5 *
FROM Posts
WHERE PostTypeId = 2
  AND OwnerUserId = @Id
ORDER BY CreationDate DESC";

            using (var connection = ConnectionHelper.GetConnection())
            using (var queryResults = connection.QueryMultiple(sql, new { Id = id }))
            {
                UserWithBadges userWithBadges = null;

                // Read<Parent, Child, Parent>(MapFunction)
                queryResults.Read<UserWithBadges, Badge, UserWithBadges>((user, badge) =>
                {
                    if (userWithBadges == null)
                    {
                        userWithBadges = user;
                    }

                    if (!userWithBadges.Badges.ContainsKey(badge.Name))
                    {
                        userWithBadges.Badges.Add(badge.Name, 0);
                    }

                    userWithBadges.Badges[badge.Name] = userWithBadges.Badges[badge.Name] + 1;

                    return user;
                }).ToList();

                ViewData.Model = userWithBadges;

                ViewBag.Questions = queryResults.Read<Post>().ToList();
                ViewBag.Answers = queryResults.Read<Post>().ToList();
            };

            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                using (var connection = ConnectionHelper.GetConnection())
                {
                    connection.Execute(@"
    INSERT INTO Users
    (Reputation, CreationDate, LastAccessDate, DisplayName, WebsiteUrl, Location, Age, AboutMe, Views, UpVotes, DownVotes)
    VALUES
    (0, GETDATE(), GETDATE(), @DisplayName, @WebsiteUrl, @Location, @Age, @AboutMe, 0, 0, 0)
    ", user);
                }

                return RedirectToAction("");
            }

            return View();
        }
    }
}
