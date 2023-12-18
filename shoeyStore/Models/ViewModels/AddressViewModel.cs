using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shoeyStore.Models.ViewModels
{
    using System;
    using System.Collections.Generic;

    public partial class AddressViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AddressViewModel()
        {
            this.Ordens = new HashSet<Orden>();
        }
        public int IDDireccion { get; set; }
        public Nullable<int> IDCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Linea { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string ZIP { get; set; }
        public string Telefono { get; set; }

        public virtual Cliente Cliente { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orden> Ordens { get; set; }
    }

}
