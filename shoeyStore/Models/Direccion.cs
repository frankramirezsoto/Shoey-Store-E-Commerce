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
    
    public partial class Direccion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Direccion()
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
