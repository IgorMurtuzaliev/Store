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
