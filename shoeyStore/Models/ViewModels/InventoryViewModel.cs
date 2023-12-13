using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shoeyStore.Models.ViewModels
{
    public class InventoryViewModel
    {
        public int IDInventario { get; set; }
        public int IDProducto { get; set; }
        public Nullable<decimal> TallaUS { get; set; }
        public Nullable<int> Cantidad { get; set; }
        public Nullable<decimal> Precio { get; set; }

        public virtual Producto Producto { get; set; }
    }
}