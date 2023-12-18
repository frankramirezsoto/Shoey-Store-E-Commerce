using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shoeyStore.Models.ViewModels
{
    public class SellerViewModel
    {
        public SellerViewModel()
        {
            this.Productoes = new HashSet<Producto>();
        }

        public int IDVendedor { get; set; }
        public string NombreVendedor { get; set; }
        public string CorreoElectronico { get; set; }
        public string Contrasenna { get; set; }
        public string IBAN { get; set; }
        public Nullable<bool> Verificado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Producto> Productoes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orden> Ordens { get; set; }
    }
}