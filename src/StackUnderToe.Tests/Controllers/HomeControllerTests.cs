using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Simple.Data;
using Simple.Data.Mocking;
using StackUnderToe.Controllers;
using Xunit;

namespace StackUnderToe.Tests.Controllers
{

    public class TestPost
    {
        public TestPost()
        {
            CreationDate = DateTime.Now;
            PostTypeId = 1;
            AcceptedAnswerId = null;
        }

        public DateTime CreationDate { get; set; }
        public int ViewCount { get; set; }
        public int? AcceptedAnswerId { get; set; }
        public int PostTypeId { get; set; }
    }

    public class HomeControllerTests
    {

        HomeController _controller = new HomeController();

        public HomeControllerTests()
        {
            MockHelper.UseMockAdapter( new InMemoryAdapter() );
        }

        [Fact]
        public void Test_HomeController_ShowsTop10Questions()
        {

            CreateFakePosts( 20 );

            var result = _controller.Index() as ViewResult;

            List<Post> model = (dynamic)result.Model;

            Assert.Equal( 10, model.Count() );
        }

        [Fact]
        public void Test_HomeController_PostsAreOrderedByViewCount()
        {
            CreateFakePosts( 10 );

            Database.Open().Posts.Insert( new TestPost { ViewCount = 100 } );

            var result = _controller.Index() as ViewResult;

            List<Post> model = (dynamic)result.Model;

            Assert.Equal( 100, model.First().ViewCount );
        }

        /// <summary>
        /// Makes some fake posts
        /// </summary>
        /// <param name="num">The number of fake posts to put in the "Database"</param>
        private void CreateFakePosts( int num )
        {
            var db = Database.Open();

            for( int i = 0; i < num; i++ )
            {
                db.Posts.Insert(new TestPost{ ViewCount = 1 });
            }
        }
    }
}
