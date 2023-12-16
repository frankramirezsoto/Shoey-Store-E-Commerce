using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shoeyStore.Models.ViewModels
{
    public class ClientViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClientViewModel()
        {
            this.Carritoes = new HashSet<Carrito>();
            this.Ordens = new HashSet<Orden>();
        }

        public int IDCliente { get; set; }
        public string NombreCliente { get; set; }
        public string CorreoElectronico { get; set; }
        public string Contrasenna { get; set; }
        public string NumeroTelefono { get; set; }
        public string Direccion { get; set; }
        public string TarjetaBancaria { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Carrito> Carritoes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orden> Ordens { get; set; }
    }
}