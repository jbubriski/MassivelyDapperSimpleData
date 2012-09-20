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

        private Dogs _dogsTable = new Dogs();

        //
        // GET: /Dogs/

        public ActionResult Index()
        {
            var topTenDogs = _dogsTable.All( orderBy: "legs DESC" );
            return View( topTenDogs );
        }

        //
        // GET: /Dogs/Details/5

        public ActionResult Details(int id)
        {
            var dog = _dogsTable.Single( id );
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
                _dogsTable.Insert( collection );

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
            var dog = _dogsTable.Single( id );
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
                _dogsTable.Update( collection, id );

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
            var dog = _dogsTable.Single( id );
            return View( dog );
        }

        //
        // POST: /Dogs/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                _dogsTable.Delete( id );

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
