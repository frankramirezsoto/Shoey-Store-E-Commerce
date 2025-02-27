﻿using shoeyStore.Models.ViewModels;
using shoeyStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shoeyStore.Controllers
{
    public class OrderController : Controller
    {
        //The Order model has to be loaded with all of the tables that are related to in the database 
        // GET: Order
        [HttpGet]
        public ActionResult Index()
        {
            //List of Orders will be populated with the database request
            List<OrderTableViewModel> listOrders = null;
            //Check on user session to see if it's logged
            var user = (Cliente)Session["Logged"];

            using (ShoeyDatabaseEntities db = new ShoeyDatabaseEntities())
            {
                //If logged then it will fill the list of products
                if (user != null)
                {
                    //Fills the list with the orders from the database
                    listOrders = (from o in db.Ordens
                                  where o.IDCliente == user.IDCliente
                                  select new OrderTableViewModel
                                  {
                                      IDOrden = o.IDOrden,
                                      IDCliente = o.IDCliente,
                                      IDDireccion = o.IDDireccion,
                                      IDTarjeta = o.IDTarjeta,
                                      FechaOrden = o.FechaOrden,
                                      MontoTotal = o.MontoTotal,
                                      Estado = o.Estado,
                                      DetallesOrdens = o.DetallesOrdens,
                                      OrderDetails = (from od in db.DetallesOrdens
                                                      where od.IDOrden == o.IDOrden
                                                      select new OrderDetailViewModel
                                                      {
                                                          IDDetalleOrden = od.IDDetalleOrden,
                                                          IDOrden = od.IDOrden,
                                                          IDInventario = od.IDInventario,
                                                          Cantidad = od.Cantidad,
                                                          IDProducto = od.IDProducto,
                                                          Producto = od.Producto,
                                                      }).ToList()
                }).ToList();

                    foreach (var o in listOrders) 
                    {
                        o.Direccion = db.Direccions.Find(o.IDDireccion);
                        o.Tarjeta = db.Tarjetas.Find(o.IDTarjeta);
                        foreach (var od in o.OrderDetails)  
                        {
                            od.Inventario = db.Inventarios.Find(od.IDInventario);
                            od.Producto = db.Productoes.Find(od.IDProducto);
                        }
                    };
                }
            }
            return View(listOrders);
        }
        //Method to post an order to the database   
        [HttpPost]
        public ActionResult Place(OrderViewModel order)
        {
            //Checks if model is valid
            if (!ModelState.IsValid) return View(order);
            //Check on user session to see if it's logged
            var user = (Cliente)Session["Logged"];
            try 
            {
                using (var db = new ShoeyDatabaseEntities())
                {
                    Orden orderTO = new Orden();
                    //If the card ID is null then it will add the new card to the database 
                    if (order.IDTarjeta == null)
                    {
                        Tarjeta cardTO = new Tarjeta
                        {
                            IDCliente = user.IDCliente,
                            Numero = order.Tarjeta.Numero,
                            Expiracion = order.Tarjeta.Expiracion,
                            CVV = order.Tarjeta.CVV,
                        };
                        db.Tarjetas.Add(cardTO);
                        db.SaveChanges();

                        orderTO.IDTarjeta = cardTO.IDTarjeta;
                    }
                    else
                    {
                        orderTO.IDTarjeta = order.IDTarjeta;
                    }

                    if (order.IDDireccion == null)
                    {
                        Direccion addressTO = new Direccion
                        {
                            Nombre = order.Direccion.Nombre,
                            Apellido = order.Direccion.Apellido,
                            Linea = order.Direccion.Linea,
                            Ciudad = order.Direccion.Ciudad,
                            Estado = order.Direccion.Estado,
                            ZIP = order.Direccion.ZIP,
                            Telefono = order.Direccion.Telefono,
                        };
                        db.Direccions.Add(addressTO);
                        db.SaveChanges();

                        orderTO.IDDireccion = addressTO.IDDireccion;
                    }
                    else
                    {
                        orderTO.IDDireccion = order.IDDireccion;
                    }
                    orderTO.FechaOrden = DateTime.Now;
                    orderTO.MontoTotal = order.MontoTotal;
                    orderTO.IDCliente = user.IDCliente;

                    db.Ordens.Add(orderTO);
                    db.SaveChanges();

                    foreach (var orderDetail in order.DetallesOrdens)
                    {
                        DetallesOrden orderDetailsTO = new DetallesOrden
                        {
                            IDOrden = orderTO.IDOrden,
                            IDProducto = orderDetail.IDProducto,
                            IDInventario = orderDetail.IDInventario,
                            Cantidad = orderDetail.Cantidad,
                        };
                        db.DetallesOrdens.Add(orderDetailsTO);
                        db.SaveChanges();
                    }
                    foreach (var cart in order.CartItems) 
                    {
                        var cartTO = db.Carritoes.Find(cart.IDCarrito);
                        db.Entry(cartTO).State = System.Data.Entity.EntityState.Deleted;
                        db.SaveChanges();
                    }
                    return Content("200");
                }
            } catch (Exception ex)
            {
                return Content("Error:" + ex);
            }
        }
    }
}