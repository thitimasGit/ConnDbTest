using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace TestConnDbCommand.Models
{
    public class MainModel
    {
        public string connStr = WebConfigurationManager.ConnectionStrings["connStrMyDB"].ConnectionString;

    }
}