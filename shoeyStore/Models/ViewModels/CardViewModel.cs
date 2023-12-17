using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shoeyStore.Models.ViewModels
{
    using System;
    using System.Collections.Generic;

    public partial class CardViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CardViewModel()
        {
            this.Ordens = new HashSet<Orden>();
        }

        public int IDTarjeta { get; set; }
        public Nullable<int> IDCliente { get; set; }
        public string Numero { get; set; }
        public Nullable<System.DateTime> Expiracion { get; set; }
        public string CVV { get; set; }

        public virtual Cliente Cliente { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orden> Ordens { get; set; }
    }
}