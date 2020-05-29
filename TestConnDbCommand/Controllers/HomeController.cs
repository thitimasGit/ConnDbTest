using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestConnDbCommand.Models;

namespace TestConnDbCommand.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            Provinces prb = new Provinces();
            DataTable dtPrv = prb.GetDt(5, 20, "");

            string htmlTableDt = "<div class='table'>";

            foreach (DataRow drx in dtPrv.Rows)
            {
                htmlTableDt += "<div class='row'>";
                htmlTableDt += "<div class='col'>";
                htmlTableDt += drx["Id"];
                htmlTableDt += "</div>";

                htmlTableDt += "<div class='col'>";
                htmlTableDt += drx["NameInThai"];
                htmlTableDt += "</div>";
                htmlTableDt += "</div>";
            }

            htmlTableDt += "</div>";

            ViewBag.Dtr1 = dtPrv.Rows;
            ViewBag.Tb1 = htmlTableDt;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}