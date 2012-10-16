﻿using System;
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

            ViewBag.UniqueBadgeCount = badgesTable.Scalar("SELECT COUNT(DISTINCT Name) FROM Badges");
            ViewBag.TotalBadgeCount = badgesTable.Count();
            
            ViewBag.PopularBadges = badgesTable.Query(@"
                                        SELECT TOP 5 Name, 
                                                COUNT(*) AS Count 
                                        FROM Badges 
                                        GROUP BY Name 
                                        ORDER BY Count DESC");

            ViewBag.UsersWithMostBadges = badgesTable.Query(@"
                                        SELECT TOP 5 b.UserId, COUNT(*) AS Count, u.DisplayName
                                        FROM Badges b
                                        JOIN Users u ON u.Id = b.UserId
                                        GROUP BY b.UserId, u.DisplayName
                                        ORDER BY Count DESC");

            return View();
        }

        public ActionResult Page( int page = 1 )
        {
            var badgesTable = new BadgesTable();

            // var pagedList = badgesTable.Paged( currentPage: page );

            var pagedList = badgesTable.Query( @"
                                                SELECT TOP 20 * FROM Badges B, RowNumber() as RowNumber, Count(*) as MaxRows
                                                INNER JOIN Users U on U.Id = B.UserId
                                                WHERE RowNumber > @0", 20 * (page-1));

            return View( pagedList );
        }

        public ActionResult Top()
        {
            var badgesTable = new BadgesTable();

            ViewBag.PopularBadges = badgesTable.Query(@"
                                        SELECT TOP 20 Name, COUNT(*) AS Count 
                                        FROM Badges 
                                        GROUP BY Name 
                                        HAVING COUNT(*) >= 500 
                                        ORDER BY Count DESC");

            return View();
        }

        public ActionResult Rare()
        {
            var badgesTable = new BadgesTable();

            ViewBag.PopularBadges = badgesTable.Query("SELECT Name, COUNT(*) AS Count FROM Badges GROUP BY Name HAVING COUNT(*) <= 5 ORDER BY Count ASC");

            return View();
        }
    }
}
