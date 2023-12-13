using shoeyStore.Models;
using shoeyStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shoeyStore.Controllers
{
    public class SellerAdminController : Controller
    {
        // GET: SellerAdmin
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Products()
        {
            List<ProductTableViewModel> listProducts = null;
            var user = (Vendedor)Session["SellerLogged"];

            using (ShoeyDatabaseEntities db = new ShoeyDatabaseEntities())
            {
                if (user != null) 
                {
                    listProducts = (from p in db.Productoes
                                    where p.IDVendedor == user.IDVendedor
                                    select new ProductTableViewModel
                                    {
                                        IDProducto = p.IDProducto,
                                        IDVendedor = p.IDVendedor,
                                        Nombre = p.Nombre,
                                        Categoria = p.Categoria,
                                        Genero = p.Genero,
                                        Marca = p.Marca,
                                        Modelo = p.Modelo,
                                        Imagen = p.Imagen,
                                        Calificaciones = p.Calificacions,
                                        DetallesOrdens = p.DetallesOrdens,
                                        Vendedor = p.Vendedor
                                    }).ToList();
                }
            }

            return View(listProducts);
        }


        [HttpGet]
        public ActionResult AddProduct()
        {   
            ProductViewModel model = new ProductViewModel();  

            return View(model);
        }

        [HttpPost]
        public ActionResult AddProduct(ProductViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            using (var db = new ShoeyDatabaseEntities())
            {
                Producto newProduct = new Producto
                {
                    IDVendedor = model.IDVendedor,
                    Nombre = model.Nombre,
                    Categoria = model.Categoria,
                    Genero = model.Genero,
                    Marca = model.Marca,
                    Modelo = model.Modelo,
                    Imagen = Convert.FromBase64String(model.ImagenBase64),
                };

                db.Productoes.Add(newProduct);
                db.SaveChanges();  

                if (model.InventoryEntries != null)
                {
                    foreach (var entry in model.InventoryEntries)
                    {
                        Inventario newInventory = new Inventario
                        {
                            IDProducto = newProduct.IDProducto,
                            TallaUS = entry.TallaUS,
                            Cantidad = entry.Cantidad,
                            Precio = entry.Precio,
                        };

                        db.Inventarios.Add(newInventory);
                    }
                }

                db.SaveChanges();

                return RedirectToAction("Products", "SellerAdmin");
            }
        }

        //[HttpGet]
        //public ActionResult EditProduct(int id)
        //{
        //    using (var db = new ShoeyDatabaseEntities())
        //    {
        //        // Retrieve the product and its associated inventory entries
        //        var product = db.Productoes.Include(p => p.Inventarios).FirstOrDefault(p => p.IDProducto == id);

        //        if (product == null)
        //        {
        //            // Handle the case where the product is not found
        //            return RedirectToAction("Products", "SellerAdmin");
        //        }

        //        // Map the product and its inventory entries to the view model
        //        var viewModel = new ProductViewModel
        //        {
        //            IDVendedor = product.IDVendedor,
        //            Nombre = product.Nombre,
        //            Categoria = product.Categoria,
        //            Genero = product.Genero,
        //            Marca = product.Marca,
        //            Modelo = product.Modelo,
        //            ImagenBase64 = Convert.ToBase64String(product.Imagen),
        //            InventoryEntries = product.Inventarios.Select(i => new InventoryViewModel
        //            {
        //                TallaUS = i.TallaUS,
        //                Cantidad = i.Cantidad,
        //                Precio = i.Precio
        //            }).ToList()
        //        };

        //        return View(viewModel);
        //    }
        //}

        //[HttpPost]
        //public ActionResult EditProduct(ProductViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        // Handle invalid model state
        //        return View(model);
        //    }

        //    using (var db = new ShoeyDatabaseEntities())
        //    {
        //        // Retrieve the existing product and associated inventory entries
        //        var existingProduct = db.Productoes.Include(p => p.Inventarios).FirstOrDefault(p => p.IDProducto == model.IDProducto);

        //        if (existingProduct == null)
        //        {
        //            // Handle the case where the product is not found
        //            return RedirectToAction("Products", "SellerAdmin");
        //        }

        //        // Update the existing product with the new information
        //        existingProduct.Nombre = model.Nombre;
        //        existingProduct.Categoria = model.Categoria;
        //        existingProduct.Genero = model.Genero;
        //        existingProduct.Marca = model.Marca;
        //        existingProduct.Modelo = model.Modelo;

        //        // Update the product image if a new one is provided
        //        if (!string.IsNullOrEmpty(model.ImagenBase64))
        //        {
        //            existingProduct.Imagen = Convert.FromBase64String(model.ImagenBase64);
        //        }

        //        // Update inventory entries
        //        if (model.InventoryEntries != null)
        //        {
        //            foreach (var entry in model.InventoryEntries)
        //            {
        //                // Find the corresponding inventory entry
        //                var existingInventory = existingProduct.Inventarios.FirstOrDefault(i => i.TallaUS == entry.TallaUS);

        //                if (existingInventory != null)
        //                {
        //                    // Update existing inventory entry
        //                    existingInventory.Cantidad = entry.Cantidad;
        //                    existingInventory.Precio = entry.Precio;
        //                }
        //                else
        //                {
        //                    // Create a new inventory entry if it doesn't exist
        //                    var newInventory = new Inventario
        //                    {
        //                        IDProducto = existingProduct.IDProducto,
        //                        TallaUS = entry.TallaUS,
        //                        Cantidad = entry.Cantidad,
        //                        Precio = entry.Precio,
        //                    };

        //                    db.Inventarios.Add(newInventory);
        //                }
        //            }
        //        }

        //        db.SaveChanges();

        //        return RedirectToAction("Products", "SellerAdmin");
        //    }
        //}

        [HttpPost]
        public ActionResult DeleteProduct(int Id)
        {
            using (var db = new ShoeyDatabaseEntities())
            {
                var productTO = db.Productoes.Find(Id);

                var inventoryItems = db.Inventarios.Where(i => i.IDProducto == Id);
                db.Inventarios.RemoveRange(inventoryItems);

                db.Entry(productTO).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }

            return Content("200");
        }
    }
}
