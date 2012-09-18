using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MassiveKennel.Models;

namespace MassiveKennel.Controllers
{
    public class DogsController : Controller
    {
        //
        // GET: /Dogs/

        public ActionResult Index()
        {
            var topTenDogs = new Dogs().All( orderBy: "legs DESC" );
            return View( topTenDogs );
        }

        //
        // GET: /Dogs/Details/5

        public ActionResult Details(int id)
        {
            var dog = new Dogs().Single( id );
            return View( dog );
        }

        //
        // GET: /Dogs/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Dogs/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                new Dogs().Insert( collection );

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Dogs/Edit/5

        public ActionResult Edit(int id)
        {
            var dog = new Dogs().Single( id );
            return View( dog );
        }

        //
        // POST: /Dogs/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                var dog = new Dogs().Single( id );
                foreach ( var key in collection.AllKeys )
                {
                    dog[key] = collection.GetValue( key );
                }

                new Dogs().Update( dog, id );

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Dogs/Delete/5

        public ActionResult Delete(int id)
        {
            var dog = new Dogs().Single( id );
            return View();
        }

        //
        // POST: /Dogs/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                new Dogs().Delete( id );

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
