using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestConnDbEf.Models;

namespace TestConnDbEf.Controllers
{
    public class EftController : Controller
    {
        private TestConnEntities db = new TestConnEntities();
        // GET: Eft
        public ActionResult Index()
        {
            // foreach (var row in db.Provinces.Where(x => x.NameInEnglish.StartWith("nakhon")).ToList() )
            // foreach (var row in db.Provinces.Where(x => x.NameInEnglish.EndsWith("nakhon")).ToList() )
            foreach (var row in db.Provinces.Where(x => x.NameInEnglish.Contains("nakhon")).ToList() )
            {
                Debug.WriteLine(row.Id + ":" + row.Code + ":" + row.NameInEnglish);

            }

            return View();
        }

        public ActionResult IndexSub()
        {
            // foreach (var row in db.Provinces.Where(x => x.NameInEnglish.StartWith("nakhon")).ToList() )
            // foreach (var row in db.Provinces.Where(x => x.NameInEnglish.EndsWith("nakhon")).ToList() )
            var listNow = from a in db.Provinces
                          join b in db.Districts on a.Id equals b.ProvinceId
                          join c in db.Subdistricts on b.Id equals c.DistrictId
                          select new { 
                              cola = a.Code
                              , colb = a.NameInEnglish
                              , colc = b.NameInEnglish
                              , cold = c.NameInEnglish
                          };
            
            foreach (var row in listNow.Where(x => x.colb.StartsWith("nakhon")).ToList())
            {
                Debug.WriteLine(row.cola + ":->" + row.colb + ":->" + row.colc + ":->" + row.cold);
            }

            return View();
        }

        // GET: Eft/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Eft/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Eft/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                //TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Eft/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Eft/Edit/5
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

        // GET: Eft/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Eft/Delete/5
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
