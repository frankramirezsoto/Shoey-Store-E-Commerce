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
            //List of Products will be populated with the database request
            List<ProductTableViewModel> listProducts = null;
            //Check on user session to obtain Seller ID
            var user = (Vendedor)Session["SellerLogged"];

            using (ShoeyDatabaseEntities db = new ShoeyDatabaseEntities())
            {
                //If logged then it will fill the list of products
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
            //Model returned to be able to populate the dropdown for gender
            ProductViewModel model = new ProductViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult AddProduct(ProductViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            //If model is valid it will connect to the database and Add a new product to the Product table with the values of the model provided 
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
                //If the model has items on the InventoryEntries list, it will add an entry to the Inventory table for each entry
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

        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            using (var db = new ShoeyDatabaseEntities())
            {
                // Retrieve the product from the database based on its id
                var product = db.Productoes.Find(id);
                // Fills a list with the Inventory items where the IDProducto matches the same as in the product to edit 
                List<InventoryViewModel> inventoryList = db.Inventarios.Where(i => i.IDProducto == product.IDProducto).Select(i => new InventoryViewModel
                {
                    IDInventario = i.IDInventario,
                    TallaUS = i.TallaUS,
                    Cantidad = i.Cantidad,
                    Precio = i.Precio,
                }).ToList();
                //If no product found, we redirect 
                if (product == null)
                {
                    // Handle the case where the product is not found
                    return RedirectToAction("Products", "SellerAdmin");
                }
                // Map the product
                var viewModel = new ProductViewModel
                {
                    IDProducto = product.IDProducto,
                    IDVendedor = product.IDVendedor,
                    Nombre = product.Nombre,
                    Categoria = product.Categoria,
                    Genero = product.Genero,
                    Marca = product.Marca,
                    Modelo = product.Modelo,
                    ImagenBase64 = Convert.ToBase64String(product.Imagen),
                    InventoryEntries = inventoryList,
                };
                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult EditProduct(ProductViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            using (var db = new ShoeyDatabaseEntities())
            {
                // Retrieve the product from the database based on its id
                var existingProduct = db.Productoes.Find(model.IDProducto);
                List<InventoryViewModel> inventoryList = db.Inventarios.Where(i => i.IDProducto == model.IDProducto).Select(i => new InventoryViewModel
                {
                    IDInventario = i.IDInventario,
                    TallaUS = i.TallaUS,
                    Cantidad = i.Cantidad,
                    Precio = i.Precio,
                }).ToList();

                if (existingProduct == null)
                {
                    // Redirects if the product is not found
                    return RedirectToAction("Products", "SellerAdmin");
                }

                // Update the existing product with the new information
                existingProduct.IDProducto = model.IDProducto;
                existingProduct.Nombre = model.Nombre;
                existingProduct.Categoria = model.Categoria;
                existingProduct.Genero = model.Genero;
                existingProduct.Marca = model.Marca;
                existingProduct.Modelo = model.Modelo;

                // Update the product image if a new one is provided
                if (!string.IsNullOrEmpty(model.ImagenBase64))
                {
                    existingProduct.Imagen = Convert.FromBase64String(model.ImagenBase64);
                }

                db.Entry(existingProduct).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                // Update inventory entries
                if (model.InventoryEntries != null)
                {

                    foreach (var entry in model.InventoryEntries)
                    {
                        // Find the corresponding inventory entry
                        var existingInventory = db.Inventarios.Find(entry.IDInventario);

                        if (existingInventory != null)
                        {
                            // Update existing inventory entry
                            existingInventory.TallaUS = entry.TallaUS;
                            existingInventory.Cantidad = entry.Cantidad;
                            existingInventory.Precio = entry.Precio;
                        }
                        else
                        {
                            // Create a new inventory entry if it doesn't exist
                            var newInventory = new Inventario
                            {
                                IDProducto = existingProduct.IDProducto,
                                TallaUS = entry.TallaUS,
                                Cantidad = entry.Cantidad,
                                Precio = entry.Precio,
                            };

                            db.Inventarios.Add(newInventory);
                            // Save changes only once after the loop
                        }
                    }

                    // Remove inventory entries that are not present in the model
                    var inventoryIds = model.InventoryEntries.Select(e => e.IDInventario).ToList();
                    var inventoriesToRemove = db.Inventarios.Where(i => i.IDProducto == existingProduct.IDProducto && !inventoryIds.Contains(i.IDInventario)).ToList();
                    foreach (var inventory in inventoriesToRemove)
                    {
                        db.Inventarios.Remove(inventory);
                    }

                    // Save changes once after the loop
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Products", "SellerAdmin");
        }


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
