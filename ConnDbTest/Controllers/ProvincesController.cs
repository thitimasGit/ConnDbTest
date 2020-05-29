using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConnDbTest.Controllers
{
    public class ProvincesController : Controller
    {
        // GET: Provinces
        public ActionResult Index()
        {
            return View();
        }

        // GET: Provinces/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Provinces/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Provinces/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Provinces/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Provinces/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Provinces/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Provinces/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
