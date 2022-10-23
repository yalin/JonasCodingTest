using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // creates log file at the beginning
            if (!File.Exists("log.csv"))
                File.Create("log.csv");
            
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
