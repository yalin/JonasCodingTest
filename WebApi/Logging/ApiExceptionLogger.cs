using System;
using System.Web.Http.ExceptionHandling;
using WebApi.Models;

namespace WebApi.Logging
{
    /// <summary>
    /// Exception logger
    /// It catches the exceptions thrown in web api, prepares it and logs it
    /// </summary>
    public class ApiExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            var strLogText = context.Exception.ToString();
            
            var requestedURi = context.Request.RequestUri.AbsoluteUri.ToString();
            var requestMethod = context.Request.Method.ToString();

            WebApiError webApiError = new WebApiError()
            {
                Message = strLogText,
                RequestUri = requestedURi,
                RequestMethod = requestMethod,
                TimeUtc = DateTime.Now
            };

            Logging logging = new Logging();
            logging.InsertLog(webApiError);
        }
    }

}