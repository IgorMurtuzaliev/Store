using MyStore.Controllers;
using MyStore.Logger;
using MyStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyStore.Filters
{
    public class MyActionAttribute : FilterAttribute, IActionFilter, IExceptionFilter
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            log4net.LogicalThreadContext.Properties["response_time"] = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt");
            log4net.LogicalThreadContext.Properties["username"] = filterContext.HttpContext.User.Identity.Name;
            log4net.LogicalThreadContext.Properties["request_uri"] = filterContext.HttpContext.Request.RawUrl;
            log4net.LogicalThreadContext.Properties["headers"] = filterContext.HttpContext.Response.Headers;
            log4net.LogicalThreadContext.Properties["status_code"] = filterContext.HttpContext.Response.StatusCode;
            log4net.LogicalThreadContext.Properties["body"] = "Body";
            Log4net.Log.Info("Message");
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            log4net.LogicalThreadContext.Properties["request_time"] = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt");
            log4net.LogicalThreadContext.Properties["username"] = filterContext.HttpContext.User.Identity.Name;
            log4net.LogicalThreadContext.Properties["request_uri"] = filterContext.HttpContext.Request.RawUrl;
            log4net.LogicalThreadContext.Properties["method"] = filterContext.HttpContext.Request.HttpMethod;
            log4net.LogicalThreadContext.Properties["headers"] = filterContext.HttpContext.Request.Headers;
            log4net.LogicalThreadContext.Properties["q_string"] = filterContext.HttpContext.Request.QueryString;
            log4net.LogicalThreadContext.Properties["body"] = "Body";
            Log4net.Log.Info("Message");
        }

        public void OnException(ExceptionContext filterContext)
        {
            log4net.LogicalThreadContext.Properties["trace"] = filterContext.Exception.StackTrace;
            log4net.LogicalThreadContext.Properties["mess"] = filterContext.Exception.Message;
            Log4net.Log.Error("Error");
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

//log4net.ILog logger = log4net.LogManager.GetLogger("DBLog");

//log4net.GlobalContext.Properties["request_time"] = DateTime.Now;
//            log4net.GlobalContext.Properties["username"] = filterContext.HttpContext.User.Identity.Name;
//            log4net.GlobalContext.Properties["request_uri"] = filterContext.HttpContext.Request.RawUrl;
//            log4net.GlobalContext.Properties["status_code"] = filterContext.HttpContext.Request.HttpMethod;
//            log4net.GlobalContext.Properties["headers"] = filterContext.HttpContext.Request.Headers;
//            log4net.GlobalContext.Properties["q_string"] = filterContext.HttpContext.Request.QueryString;

//            Log4net.Log.Info("mes");


//log4net.ILog logger = log4net.LogManager.GetLogger(typeof(HomeController));  //Declaring Log4Net
//MyLogger log = new MyLogger
//{
//    RequestTime = DateTime.Now,
//    Username = filterContext.HttpContext.User.Identity.Name,
//    RequestUri = filterContext.HttpContext.Request.RawUrl,
//    StatusCode = filterContext.HttpContext.Request.HttpMethod,
//    Headers = filterContext.HttpContext.Request.Headers.ToString(),
//    QueryString = filterContext.HttpContext.Request.QueryString.ToString()
//};
//db.MyLoggers.Add(log);
//            db.SaveChanges();
//            logger.Info(log);