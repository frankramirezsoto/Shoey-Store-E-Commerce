using shoeyStore.Models.ViewModels;
using shoeyStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shoeyStore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new ShoeyDatabaseEntities())
            {
                // Start with all products
                IQueryable<ProductTableViewModel> query = db.Productoes
                    .Select(p => new ProductTableViewModel
                    {
                        IDProducto = p.IDProducto,
                        IDVendedor = p.IDVendedor,
                        Nombre = p.Nombre,
                        Categoria = p.Categoria,
                        Genero = p.Genero,
                        Marca = p.Marca,
                        Modelo = p.Modelo,
                        Imagen = p.Imagen,
                        InventoryEntries = db.Inventarios.Where(i => i.IDProducto == p.IDProducto).Select(i => new InventoryViewModel
                        {
                            IDInventario = i.IDInventario,
                            TallaUS = i.TallaUS,
                            Cantidad = i.Cantidad,
                            Precio = i.Precio,
                        }).ToList(),
                        Calificaciones = p.Calificacions,
                        DetallesOrdens = p.DetallesOrdens,
                        Vendedor = p.Vendedor
                    });


                // Now 'query' contains the filtered products
                var products = query.ToList();

                // You can pass the 'products' to the view
                return View(products);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}