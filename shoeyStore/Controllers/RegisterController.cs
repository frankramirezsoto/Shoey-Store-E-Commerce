using shoeyStore.Models;
using shoeyStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shoeyStore.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(ClientViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            using (var db = new ShoeyDatabaseEntities())
            {
                    Cliente clientTO = new Cliente();
                    clientTO.NombreCliente = model.NombreCliente;
                    clientTO.NumeroTelefono = model.NumeroTelefono;
                    clientTO.CorreoElectronico = model.CorreoElectronico;
                    clientTO.Contrasenna = model.Contrasenna;

                    db.Clientes.Add(clientTO);
                    db.SaveChanges();

                    return Redirect(Url.Content("~/Home/Index?showToast=registrationSuccessful"));
            }
        }
    }
}