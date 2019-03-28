using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Diagnostics;
using System.Xml.Schema;
using Backend.Models;
using FreelanceLand.Models;
using Microsoft.AspNetCore.Hosting.Internal;

namespace Backend.Common
{
    public class LogAttribute : ActionFilterAttribute
    {
        private EFGenericRepository<ExceptionLog> exeRepo;
        private readonly ApplicationContext db;

        public LogAttribute(ApplicationContext context)
        {
            db = context;
            exeRepo = new EFGenericRepository<ExceptionLog>(context);
        }

        public LogAttribute()
        {
            
        }

        
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Log("OnActionExecuted", filterContext.RouteData);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Log("OnActionExecuting", filterContext.RouteData);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            Log("OnResultExecuted", filterContext.RouteData);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            Log("OnResultExecuting", filterContext.RouteData);
        }

        private async void Log(string methodName, RouteData routeData)
        {
            ExceptionLog log = new ExceptionLog();
            log.Level = routeData.Values["controller"].ToString();
            log.StackTrace = routeData.Values["action"].ToString();
            log.Logger = methodName;
            log.TimeStamp=DateTime.Now;
            await exeRepo.CreateAsync(log);
        }
    }
}


