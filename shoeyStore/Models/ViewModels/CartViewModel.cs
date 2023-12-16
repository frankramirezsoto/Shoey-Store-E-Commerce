using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shoeyStore.Models.ViewModels
{
    public class CartViewModel
    {
        public int IDCarrito { get; set; }
        public Nullable<int> IDCliente { get; set; }
        public Nullable<int> IDProducto { get; set; }
        public Nullable<int> IDInventario { get; set; }
        public Nullable<int> Cantidad { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual Inventario Inventario { get; set; }
        public virtual Producto Producto { get; set; }

    }
}