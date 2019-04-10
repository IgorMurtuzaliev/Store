using MyStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyStore.Filters
{
    public class MyActionAttribute : FilterAttribute, IActionFilter
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger("");

            log4net.LogicalThreadContext.Properties["request_time"] = DateTime.Now;
            log4net.LogicalThreadContext.Properties["username"] = filterContext.HttpContext.User.Identity.Name;
            log4net.LogicalThreadContext.Properties["request_uri"] = filterContext.HttpContext.Request.RawUrl;
            log4net.LogicalThreadContext.Properties["status_code"] = filterContext.HttpContext.Request.HttpMethod;
            log4net.LogicalThreadContext.Properties["headers"] = filterContext.HttpContext.Request.Headers;
            log4net.LogicalThreadContext.Properties["q_string"] = filterContext.HttpContext.Request.QueryString;

            logger.Info("");
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {

        }
    }
}

//MyLogger log = new MyLogger
//{
//    RequestTime = DateTime.Now,
//    Username = filterContext.HttpContext.User.Identity.Name,
//    RequestUri = filterContext.HttpContext.Request.RawUrl,
//    StatusCode = filterContext.HttpContext.Request.HttpMethod,
//    Headers = filterContext.HttpContext.Request.Headers.ToString(),
//    QueryString = filterContext.HttpContext.Request.QueryString.ToString()
//};

//log4net.LogicalThreadContext.Properties["request_time"] = log.RequestTime;
//log4net.LogicalThreadContext.Properties["username"] = log.Username;
//log4net.LogicalThreadContext.Properties["request_uri"] = log.RequestUri;
//log4net.LogicalThreadContext.Properties["status_code"] = log.StatusCode;
//log4net.LogicalThreadContext.Properties["headers"] = log.Headers;
//log4net.LogicalThreadContext.Properties["q_string"] = log.QueryString;  


//log4net.LogicalThreadContext.Properties["request_time"] = DateTime.Now;
//            log4net.LogicalThreadContext.Properties["username"] = filterContext.HttpContext.User.Identity.Name;
//            log4net.LogicalThreadContext.Properties["request_uri"] = filterContext.HttpContext.Request.RawUrl;
//            log4net.LogicalThreadContext.Properties["status_code"] = filterContext.HttpContext.Request.HttpMethod;
//            log4net.LogicalThreadContext.Properties["headers"] = filterContext.HttpContext.Request.Headers;
//            log4net.LogicalThreadContext.Properties["q_string"] = filterContext.HttpContext.Request.QueryString;          