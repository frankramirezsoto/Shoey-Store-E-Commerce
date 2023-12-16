using shoeyStore.Models;
using shoeyStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shoeyStore.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            //List of Orders will be populated with the database request
            List<CartViewModel> CartItems = null;

            //Check on user session to see if it's logged
            var user = (Cliente)Session["Logged"];

            using (ShoeyDatabaseEntities db = new ShoeyDatabaseEntities())
            {
                if (user != null)
                {
                    CartItems = (from c in db.Carritoes
                                 where c.IDCliente == user.IDCliente
                                 select new CartViewModel
                                 {
                                     IDCarrito = c.IDCarrito,
                                     IDCliente = c.IDCliente,
                                     IDProducto = c.IDProducto,
                                     IDInventario = c.IDInventario,
                                     Cantidad = c.Cantidad,
                                 }).ToList();
                    foreach(var cartItem in CartItems) 
                    {
                        cartItem.Cliente = user;
                        cartItem.Inventario = db.Inventarios.Find(cartItem.IDInventario);
                        cartItem.Producto = db.Productoes.Find(cartItem.IDProducto);
                    }
                }
            }
            CartItems?.Reverse();
            return PartialView(CartItems);
        }

        [HttpPost]
        public ActionResult Add(CartViewModel model)
        {
            if (!ModelState.IsValid) return Content("The item could not be added.");

            using (var db = new ShoeyDatabaseEntities())
            {
                //First we get the Inventory to confirm there are existing items
                Inventario inventory = db.Inventarios.Find(model.IDInventario);
                if (inventory != null)
                {
                    if (inventory.Cantidad >= model.Cantidad) //Checks if the quantity in Inventory is higher or equal than the one from the Model
                    {
                        Carrito cart = new Carrito();
                        cart.IDCliente = model.IDCliente;
                        cart.IDProducto = model.IDProducto;
                        cart.IDInventario = model.IDInventario;
                        cart.Cantidad = model.Cantidad;

                        db.Carritoes.Add(cart);
                        db.SaveChanges();

                        return Content("Nice pick!");
                    }
                }
            }
            return Content("We're sorry, but it looks like we don't have enough in stock");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (var db = new ShoeyDatabaseEntities())
            {
                try 
                {
                    var cartTO = db.Carritoes.Find(id);

                    db.Entry(cartTO).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();

                    return Content("200");
                } catch (Exception ex) {
                    return Content("Error:" + ex);
                }
            }
        }
    }
}