using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestConnDbCommand.Models;

namespace TestConnDbCommand.Controllers
{
    public class PrvController : Controller
    {
        // GET: Prv
        public ActionResult Index()
        {
            Provinces prb = new Provinces();
            DataTable dtPrv = prb.GetDt(5, 200, "");
            ViewBag.Dtr1 = dtPrv.Rows;
            Session["FirstName"] = "userSessionTest1";

            Response.Cookies["member"]["user"] = "userName1";
            Response.Cookies["member"]["fName"] = "somchai";
            Response.Cookies["member"].Expires = DateTime.Now.AddSeconds(30);

            DateTime thisDate1 = DateTime.Now;
            string mainPath = Server.MapPath("/");
            string mainPath2 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string logFileName = "log-" + thisDate1.ToString("yyyyMMdd_HHmm", new CultureInfo("en-us"));
            string logPath = mainPath + @"Logs\" + logFileName + ".txt";

            ViewBag.RsTest = mainPath + " <br> " + mainPath2 + " <br> ";

            if (System.IO.File.Exists(logPath))
            {
                ViewBag.RsTest += System.IO.File.ReadLines(logPath).Last();

                ViewBag.RsTest += "<br>---<br>";

                ViewBag.RsTest += "<br>---<br>";

                ViewBag.RsTest += "<br>---<br>";

                using (StreamReader outputFile = new StreamReader(logPath, true))
                {
                    /*while (outputFile.EndOfStream == false)
                    {
                        ViewBag.RsTest += "<br>"+outputFile.ReadLine();
                    }*/

                    ViewBag.RsTest += "<br>---<br>" + outputFile.ReadToEnd();
                }
            }
            else
            {
                ViewBag.RsTest += "<br>File Not Exist!<br>";
            }

            mainPath += thisDate1.ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("en-us"));
            string logMsg = thisDate1.ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("en-us")) + " >>> v1";
            using (StreamWriter outputFile = new StreamWriter(logPath, true))
            {
                outputFile.WriteLine(logMsg);
            }

            return View();
        }

        // GET: Prv/Details/5
        public ActionResult Details(int id)
        {

            //Response.Cookies.Remove("member");
            //Request.Cookies.Remove("member");
            //Response.Cookies.Set(cookie);
            Response.Cookies["member"].Expires = DateTime.Now.AddSeconds(-1);


            Session.Timeout = 100;
            Session.Remove("FirstName");
            Provinces prv = new Provinces();
            DataRow prvRow = prv.GetRow(id);
            ViewBag.Dr = prvRow;

            if (prvRow != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
            
        }

        public ActionResult Report()
        {
            return View();
        }

        // GET: Prv/Create
        public ActionResult Create(int id)
        {
            Provinces prv = new Provinces();
            var insRs = prv.AddRow(id, "ขขข", "bbb");
            ViewBag.InsRs = (string)Session["FirstName"] + Newtonsoft.Json.JsonConvert.SerializeObject(insRs);

            return View();
        }

        public ActionResult Create2()
        {
            ViewBag.InsData = "NoData";
            return View();
        }

        // POST: Prv/Create
        [HttpPost]
        public ActionResult Create2(FormCollection collection)
        {
            ViewBag.InsData = "DataIs:"+ collection["NameInThai"] + "::" + Newtonsoft.Json.JsonConvert.SerializeObject(collection.AllKeys);

            

            foreach (string key in collection.AllKeys)
            {
                ViewBag.InsData += "Key = " + key + "  ";
                ViewBag.InsData += "Value = " + collection[key];
                ViewBag.InsData += "<br/>";
            }

            HttpPostedFileBase fileUpload = Request.Files["myfile"];
            if ((fileUpload != null) && (fileUpload.ContentLength > 0) && !string.IsNullOrEmpty(fileUpload.FileName))
            {
                ViewBag.InsData += "<br/>FName:" + fileUpload.FileName;
                ViewBag.InsData += "<br/>FSize:" + fileUpload.ContentLength;

                string mainPath = Server.MapPath("/");
                string fullPath = mainPath+"Uploads/"+ fileUpload.FileName;
                fileUpload.SaveAs(fullPath);
            }

            return View();
        }

        // POST: Prv/Create
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

        // GET: Prv/Edit/5
        public ActionResult Edit(int id)
        {
            Session.Timeout = 105;
            Provinces prv = new Provinces();
            var insRs = prv.EditRow(id, 99, "ขขข", "bbb");
            ViewBag.InsRs = Newtonsoft.Json.JsonConvert.SerializeObject(insRs);
            return View();
        }

        // POST: Prv/Edit/5
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

        // GET: Prv/Delete/5
        public ActionResult Delete(int id)
        {
            Provinces prv = new Provinces();
            var insRs = prv.DelRow(id);
            ViewBag.InsRs = Newtonsoft.Json.JsonConvert.SerializeObject(insRs);
            return View();
        }

        // POST: Prv/Delete/5
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

        public ActionResult Download(string fileName, string fileSurName)
        {
            //string fileName = id;

            string mainPath = Server.MapPath("/");
            string fileFullName = fileName + "." + fileSurName;
            //string fileName = "download_test_1.txt";
            string fullPath = mainPath + "Downloads/" + fileFullName;
            if (System.IO.File.Exists(fullPath)) { 
                byte[] fileBytes = System.IO.File.ReadAllBytes(fullPath);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileFullName);
            }
            else
            {
                return HttpNotFound("File not found naja!");
            }
        }
    }
}
