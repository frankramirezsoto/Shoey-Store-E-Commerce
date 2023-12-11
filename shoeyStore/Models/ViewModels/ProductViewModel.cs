using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shoeyStore.Models.ViewModels
{

    public class ProductViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductViewModel()
        {
            this.Calificaciones = new HashSet<Calificacione>();
            this.DetallesOrdens = new HashSet<DetallesOrden>();
        }

        public int IDProducto { get; set; }
        public Nullable<int> IDVendedor { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public string Genero { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string TipoTalla { get; set; }
        public string TallaUS { get; set; }
        public Nullable<decimal> Precio { get; set; }
        public Nullable<int> Cantidad { get; set; }
        public Nullable<bool> Estado { get; set; }
        public byte[] Imagen { get; set; }
        public string ImagenBase64 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Calificacione> Calificaciones { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetallesOrden> DetallesOrdens { get; set; }
        public virtual Vendedor Vendedor { get; set; }

        public IEnumerable<SelectListItem> GenderList =>
            new List<SelectListItem>
            {
                new SelectListItem { Value = "Male", Text = "Male" },
                new SelectListItem { Value = "Female", Text = "Female" },
                new SelectListItem { Value = "Unisex", Text = "Unisex" }
                // Add more options as needed
            };

        public IEnumerable<SelectListItem> SizeList =>
            new List<SelectListItem>
            {
                new SelectListItem { Value = "5", Text = "Size 5" },
                new SelectListItem { Value = "6", Text = "Size 6" },
                new SelectListItem { Value = "7", Text = "Size 7" }
            };

        public IEnumerable<SelectListItem> CategoryList =>
            new List<SelectListItem>
            {
                new SelectListItem { Value = "Sneakers", Text = "Sneakers" },
            };
    }
}