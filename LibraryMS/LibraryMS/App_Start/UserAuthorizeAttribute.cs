using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PropertyMS.App_Start
{
    public class UserAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //判断是否跳过授权过滤器
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
               || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                return;
            }

            //判断登录情况
            if (filterContext.HttpContext.Session["UserName"] == null || 
                filterContext.HttpContext.Session["UserName"].ToString() == "")
            {
                //HttpContext.Current.Response.Write("认证不通过");
                //HttpContext.Current.Response.End();
                filterContext.Result = new RedirectResult("/Home/Login");
            }
        }
    }
}