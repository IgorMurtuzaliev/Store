using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyStore.Models
{
    public class MyLogger
    {
        public int Id { get; set; }
        public string RequestTime { get; set; }
        public string ResponseTime { get; set; } 
        public string Username { get; set; }
        public string RequestUri { get; set; }
        public string StatusCode { get; set; }
        public string HttpMethod { get; set; }
        public string Headers { get; set; }
        public string Body { get; set; }
        public string QueryString { get; set; }
        public string StackTrace { get; set; }
        public string Message { get; set; }
    }
}