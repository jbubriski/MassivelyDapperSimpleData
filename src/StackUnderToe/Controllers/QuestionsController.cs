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

        public ActionResult Top()
        {
            using( var connection = ConnectionHelper.GetConnection() )
            {
                var posts = connection.Query<Post>( @"
                                SELECT TOP (@Top) *
                                FROM Posts
                                Where PostTypeId = 1
                                    AND ViewCount > 0
                                    AND CommentCount > 0
                                ORDER BY ViewCount DESC, CommentCount DESC", new { Top = 20 } );

                return View( posts );
            }
        }

        public ActionResult Details( int id )
        {
            using( var connection = ConnectionHelper.GetConnection() )
            {
                // TODO: write snippet!
                var post = connection.Query<Post>( @"
                    SELECT * 
                    FROM Posts
                    WHERE Id = @Id", new { Id = id }  ).FirstOrDefault();

                var owner = connection.Query<User>( @"
                    SELECT *
                    FROM Users
                    WHERE Id = @Id", new { Id = post.OwnerUserId } ).FirstOrDefault();

                var answers = connection.Query<Post>( @"
                    SELECT *
                    FROM Posts
                    Where ParentId = @Id", new { Id = id } );

                ViewBag.Owner = owner;
                ViewBag.Answers = answers;

                return View( post );
            }
        }
    }
}
