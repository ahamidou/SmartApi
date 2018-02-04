using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace SmartApi
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            MvcHandler.DisableMvcResponseHeader = true;
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.LocalOnly;
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError() as HttpException;
            var code = exception?.GetHttpCode();
            if (code != 400) return;
            Response.Clear();
            Response.Redirect("/");
            Server.ClearError();
        }
    }
}