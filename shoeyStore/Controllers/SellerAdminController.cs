using shoeyStore.Models;
using shoeyStore.Models.ViewModels;
using System;
using System.Collections.Generic;
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


        public ActionResult Products(int SellerId)
        {
            List<ProductTableViewModel> listProducts = null;

            using (ShoeyDatabaseEntities db = new ShoeyDatabaseEntities())
            {
                var productsFromDb = (from p in db.Productoes
                                      where p.IDVendedor == SellerId
                                      select new ProductTableViewModel
                                      {
                                          IDProducto = p.IDProducto,
                                          IDVendedor = p.IDVendedor,
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
                                          Calificaciones = p.Calificaciones,
                                          DetallesOrdens = p.DetallesOrdens,
                                          Vendedor = p.Vendedor
                                      }).ToList();

                // Perform the conversion of the image in-memory
                listProducts = productsFromDb.Select(p => new ProductTableViewModel
                {
                    IDProducto = p.IDProducto,
                    IDVendedor = p.IDVendedor,
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
                    ImagenBase64 = Convert.ToBase64String(p.Imagen),
                    Calificaciones = p.Calificaciones,
                    DetallesOrdens = p.DetallesOrdens,
                    Vendedor = p.Vendedor
                }).ToList();
            }

            return View(listProducts);
        }


        [HttpGet]
        public ActionResult AddProduct()
        {
            return View();
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
                        TipoTalla = model.TipoTalla,
                        TallaUS = model.TallaUS,
                        Precio = model.Precio,
                        Cantidad = model.Cantidad,
                        Estado = model.Estado,
                        // Convert base64-encoded string to byte array for image
                        Imagen = Convert.FromBase64String(model.ImagenBase64),
                    };

                    db.Productoes.Add(newProduct);
                    db.SaveChanges();

                    return RedirectToAction("Products", "SellerAdmin");
                }
        }


        public ActionResult EditProduct(int id)
        {
            using (var db = new ShoeyDatabaseEntities())
            {
                Producto existingProduct = db.Productoes.Find(id);
                if (existingProduct == null) return HttpNotFound();
                var model = new ProductViewModel
                {
                    IDProducto = existingProduct.IDProducto,
                    IDVendedor = existingProduct.IDVendedor,
                    Nombre = existingProduct.Nombre,
                    Categoria = existingProduct.Categoria,
                    Genero = existingProduct.Genero,
                    Marca = existingProduct.Marca,
                    Modelo = existingProduct.Modelo,
                    TipoTalla = existingProduct.TipoTalla,
                    TallaUS = existingProduct.TallaUS,
                    Precio = existingProduct.Precio,
                    Cantidad = existingProduct.Cantidad,
                    Estado = existingProduct.Estado,
                    // Convert the image byte array to base64 for editing
                    ImagenBase64 = Convert.ToBase64String(existingProduct.Imagen),
                };

                return View(model);
            }
        }

        // POST: SellerAdmin/EditProduct/5
        [HttpPost]
        public ActionResult EditProduct(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ShoeyDatabaseEntities())
                {
                    Producto existingProduct = db.Productoes.Find(model.IDProducto);
                    if (existingProduct == null) return HttpNotFound();

                    existingProduct.Nombre = model.Nombre;
                    existingProduct.Categoria = model.Categoria;
                    existingProduct.Genero = model.Genero;
                    existingProduct.Marca = model.Marca;
                    existingProduct.Modelo = model.Modelo;
                    existingProduct.TipoTalla = model.TipoTalla;
                    existingProduct.TallaUS = model.TallaUS;
                    existingProduct.Precio = model.Precio;
                    existingProduct.Cantidad = model.Cantidad;
                    existingProduct.Estado = model.Estado;

                    existingProduct.Imagen = Convert.FromBase64String(model.ImagenBase64);

                    db.SaveChanges();
                    return RedirectToAction("Products", "SellerAdmin");
                }
            }
            return View(model);
        }
    }
}
