﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DtdcCashCounter.Models
{
    public class SessionTimeout : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            if (HttpContext.Current.Session["EmpId"] == null || HttpContext.Current.Session["pfCode"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                      new RouteValueDictionary(
                          new
                          {
                              controller = "Employee",
                              action = "EmpLogin",
                              returnUrl = filterContext.HttpContext.Request.Url.GetComponents(UriComponents.PathAndQuery, UriFormat.SafeUnescaped)
                          }));
            }



            base.OnActionExecuting(filterContext);
        }
    }
}