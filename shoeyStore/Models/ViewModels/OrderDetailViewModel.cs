using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shoeyStore.Models.ViewModels
{
    public class OrderDetailViewModel
    {
        public int IDDetalleOrden { get; set; }
        public Nullable<int> IDOrden { get; set; }
        public Nullable<int> IDProducto { get; set; }
        public Nullable<int> Cantidad { get; set; }

        public virtual Orden Orden { get; set; }
        public virtual Producto Producto { get; set; }
        public ProductViewModel Product { get; set; }
    }
}