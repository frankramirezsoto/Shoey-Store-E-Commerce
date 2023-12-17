using shoeyStore.Models;
using shoeyStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shoeyStore.Controllers
{
    public class SettingsController : Controller
    {
        // GET: Settings
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Access()
        {
            return View();
        }

        public ActionResult Shipping()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Payment()
        {
            //List of Orders will be populated with the database request
            List<CardViewModel> cardsList = new List<CardViewModel>();
            //Check on user session to see if it's logged
            var user = (Cliente)Session["Logged"];

            using (ShoeyDatabaseEntities db = new ShoeyDatabaseEntities())
            {
                if (user != null)
                {
                    user = db.Clientes.Include("Tarjetas").FirstOrDefault(u => u.IDCliente == user.IDCliente);
                    foreach (var card in user.Tarjetas)
                    {
                        card.Cliente = user;
                    }
                    Session["Logged"] = user;

                    foreach(var card in user.Tarjetas) 
                    {
                        CardViewModel cardTO = new CardViewModel 
                        { 
                            IDTarjeta = card.IDTarjeta,
                            IDCliente = card.IDCliente,
                            Numero = card.Numero,
                            Expiracion = card.Expiracion,
                            CVV = card.CVV,
                        };
                        cardsList.Add(cardTO);
                    }

                    return View(cardsList);
                }
            }

            return View();
        }

        public ActionResult Orders()
        {
            return View();
        }
    }
}