//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace shoeyStore.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Carrito
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
