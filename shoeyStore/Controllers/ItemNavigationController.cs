﻿using shoeyStore.Models;
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
        public ActionResult Index(string gender, string brand, decimal? minPrice, decimal? maxPrice)
        {
            // ViewBag to pass the list of brands to the view
            var allBrands = GetAllBrands();
            ViewBag.Brands = allBrands;

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

                // Apply filters
                if (!string.IsNullOrEmpty(gender))
                {
                    query = query.Where(p => p.Genero == gender);
                }

                if (!string.IsNullOrEmpty(brand))
                {
                    query = query.Where(p => p.Marca == brand);
                }

                if (minPrice.HasValue)
                {
                    query = query.Where(p => p.InventoryEntries.Any(i => i.Precio >= minPrice));
                }

                if (maxPrice.HasValue)
                {
                    query = query.Where(p => p.InventoryEntries.Any(i => i.Precio <= maxPrice));
                }

                // Now 'query' contains the filtered products
                var products = query.ToList();
                Random rng = new Random();
                products.Sort((a, b) => rng.Next(3) - 1);
                // You can pass the 'products' to the view
                return View(products);
            }
        }

        private List<SelectListItem> GetAllBrands()
        {
            using (var db = new ShoeyDatabaseEntities())
            {
                var uniqueBrands = db.Productoes.Select(p => p.Marca).Distinct().ToList();

                // Convert strings to SelectListItem
                var brandItems = uniqueBrands.Select(brand => new SelectListItem
                {
                    Text = brand,
                    Value = brand
                }).ToList();

                return brandItems;
            }
        }
    }
}