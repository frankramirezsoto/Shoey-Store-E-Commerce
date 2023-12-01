using shoeyStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shoeyStore.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string Email, string Password)
        {
            try
            {
                using (var db = new ShoeyDatabaseEntities())
                {
                    var list = from user in db.Clientes
                              where user.CorreoElectronico == Email && user.Contrasenna == Password
                              select user;

                    if (list.Count() > 0)
                    {
                        Cliente user = list.First();
                        Session["Logged"] = user;
                        return Content("200");
                    }
                    else
                    {
                        return Content("Invalid username or password");
                    }
                }
            }
            catch (Exception ex)
            {
                return Content("The following error has occurred: " + ex.Message);
            }
        }

        public ActionResult Logout()
        {
            Session["Logged"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}