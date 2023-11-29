using shoeyStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shoeyStore.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        public BaseController()
        {
            var usuarioTO = (Cliente) System.Web.HttpContext.Current.Session["Usuario"];

            if (usuarioTO != null)
            {
                ViewBag.userSession = $"{usuarioTO.NombreCliente}";
            }

        }
    }
}