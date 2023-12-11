using shoeyStore.Models;
using shoeyStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace shoeyStore.Controllers
{
    public class ItemNavigationController : Controller
    {
        // GET: ItemNavigation
        public ActionResult Index(string gender, string brand, string size)
        {

            List<ProductTableViewModel> listProducts = null;

            using (ShoeyDatabaseEntities db = new ShoeyDatabaseEntities())
            {
                var query = from e in db.Productoes select e;

                if (!string.IsNullOrEmpty(gender))
                {
                    query = query.Where(p => p.Genero == gender);
                }

                if (!string.IsNullOrEmpty(brand))
                {
                    query = query.Where(p => p.Marca == brand);
                }

                if (!string.IsNullOrEmpty(size))
                {
                    query = query.Where(p => p.TallaUS == size);
                }


                listProducts = query.Select(p => new ProductTableViewModel
                {
                    IDProducto = p.IDProducto,
                    Nombre = p.Nombre,
                    Categoria = p.Categoria,
                    Genero = p.Genero,
                    Marca = p.Marca,
                    Modelo = p.Modelo,
                    TipoTalla = p.TipoTalla,
                    TallaUS = p.TallaUS,
                    Precio = p.Precio,
                    Cantidad = p.Cantidad,
                    Estado = p.Estado,
                    Imagen = p.Imagen,
                }).ToList();
            }

            return View(listProducts);
        }
    }
}