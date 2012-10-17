using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackUnderToe.Models;

namespace StackUnderToe.Controllers
{
    public class BadgesController : Controller
    {
        public ActionResult Index()
        {
            var badgesTable = new BadgesTable();

            ViewBag.UniqueBadgeCount = badgesTable.Scalar(@"
                SELECT COUNT(DISTINCT Name)
                FROM Badges");
            
            ViewBag.TotalBadgeCount = badgesTable.Count();

            ViewBag.RecentBadges = badgesTable.All( orderBy: "creationdate DESC", limit: 10 );

            ViewBag.PopularBadges = badgesTable.Query(@"
                SELECT TOP 5 Name,
                    COUNT(*) AS Count
                FROM Badges
                GROUP BY Name
                ORDER BY Count DESC");

            ViewBag.UsersWithMostBadges = badgesTable.Query(@"
                SELECT TOP 5 b.UserId,
                    COUNT(*) AS Count,
                    u.DisplayName
                FROM Badges b
                JOIN Users u ON u.Id = b.UserId
                GROUP BY b.UserId, u.DisplayName
                ORDER BY Count DESC");

            return View();
        }

        [HttpPost]
        public ActionResult Create( FormCollection form )
        {
            var badgesTable = new BadgesTable();

            form.Add( "creationdate", DateTime.Now.ToString() );

            var badge = badgesTable.Insert( form ); // TODO: get rid of mass assignment

            return RedirectToAction( "Index" );
        }

        public ActionResult Top()
        {
            var badgesTable = new BadgesTable();

            // TODO: change to parameterized sql (TOP), COUNT >=
            ViewBag.PopularBadges = badgesTable.Query( @"
                SELECT TOP 20 Name,
                    COUNT(*) AS Count
                FROM Badges
                GROUP BY Name
                HAVING COUNT(*) >= 500
                ORDER BY Count DESC");

            return View();
        }

        public ActionResult Rare()
        {
            var badgesTable = new BadgesTable();

            ViewBag.PopularBadges = badgesTable.Query(@"
                SELECT Name,
                    COUNT(*) AS Count
                FROM Badges
                GROUP BY Name
                HAVING COUNT(*) <= 5
                ORDER BY Count ASC");

            return View();
        }
    }
}
