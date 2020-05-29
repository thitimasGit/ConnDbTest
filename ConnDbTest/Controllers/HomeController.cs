using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace ConnDbTest.Controllers
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

            string connStr = WebConfigurationManager.ConnectionStrings["connStrMyDB"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            bool connStat = false;
            try
            {
                conn.Open();
                connStat = true;
                conn.Close();
            }
            catch (SqlException)
            {
                connStat = false;
            }

            //ViewBag.TestRow1_1 = connStat;

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string cmdText = "SELECT * FROM Provinces";
            SqlCommand cmdObj = new SqlCommand(cmdText, conn);
            SqlDataAdapter ad = new SqlDataAdapter(cmdObj);
            ad.Fill(ds);
            ad.Fill(10, 5, dt);

            ViewBag.Ds = JsonConvert.SerializeObject(ds.Tables[0]);
            ViewBag.Dt = dt;

            string htmlTableDt = "<div class='table'>";

                foreach (DataRow drx in dt.Rows)
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

            string htmlTable = "<div class='table'>";
            foreach (DataTable table in ds.Tables)
            {
                foreach (DataRow dr in table.Rows)
                {
                    htmlTable += "<div class='row'>";
                        htmlTable += "<div class='col'>";
                            htmlTable += dr.Field<int>("Id");
                        htmlTable += "</div>";

                        htmlTable += "<div class='col'>";
                            htmlTable += dr.Field<string>("NameInThai");
                        htmlTable += "</div>";
                    htmlTable += "</div>";
                }
            }
            htmlTable += "</div>";

            ViewBag.ConnStr = connStr;
            ViewBag.TestRow1_1 = connStat+":"+ds.Tables[0].Rows.Count+"<br>"+htmlTable;
            ViewBag.Table1 = htmlTable;
            ViewBag.Table2 = htmlTableDt;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}