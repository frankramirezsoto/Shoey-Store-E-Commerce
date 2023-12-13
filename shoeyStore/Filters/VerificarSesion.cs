using shoeyStore.Controllers;
using shoeyStore.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shoeyStore.Filters
{
    public class VerificarSesion : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var usuarioTO = (Vendedor)HttpContext.Current.Session["SellerLogged"];

            if (usuarioTO == null)
            {
                if (filterContext.Controller is SellerAdminController == true)
                {
                    filterContext.HttpContext.Response.Redirect("~/SellerLogin/Index");
                }
            }

            base.OnActionExecuting(filterContext);
        }

    }
}