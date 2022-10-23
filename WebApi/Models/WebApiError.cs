using System;

namespace WebApi.Models
{
    public class WebApiError
    {
        public string Message { get; set; }
        public string RequestMethod { get; set; }
        public string RequestUri { get; set; }
        public DateTime TimeUtc { get; set; }
    }
}