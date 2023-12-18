using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shoeyStore.Models.ViewModels
{
    public class OrderTableViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrderTableViewModel()
        {
            this.DetallesOrdens = new HashSet<DetallesOrden>();
            this.CartItems = new HashSet<CartViewModel>();
        }

        public int IDOrden { get; set; }
        public Nullable<int> IDCliente { get; set; }
        public Nullable<int> IDDireccion { get; set; }
        public Nullable<int> IDTarjeta { get; set; }
        public Nullable<System.DateTime> FechaOrden { get; set; }
        public Nullable<decimal> MontoTotal { get; set; }
        public string Estado { get; set; }

        public virtual Cliente Cliente { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetallesOrden> DetallesOrdens { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CartViewModel> CartItems { get; set; }
        public virtual Direccion Direccion { get; set; }
        public virtual Orden Orden1 { get; set; }
        public virtual Orden Orden2 { get; set; }
        public virtual Tarjeta Tarjeta { get; set; }

        public List<OrderDetailViewModel> OrderDetails { get; set; }
    }
}