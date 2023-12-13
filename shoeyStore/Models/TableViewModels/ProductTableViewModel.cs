﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shoeyStore.Models.ViewModels
{
    public class ProductTableViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductTableViewModel()
        {
            this.Calificaciones = new HashSet<Calificacion>();
            this.DetallesOrdens = new HashSet<DetallesOrden>();
        }

        public int IDProducto { get; set; }
        public Nullable<int> IDVendedor { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public string Genero { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public byte[] Imagen { get; set; }

        public string ItemName
        {
            get
            {
                return $"{Marca} {Modelo}";
            }

            set => ItemName = value;
        }
        public List<InventoryViewModel> InventoryEntries { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Calificacion> Calificaciones { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetallesOrden> DetallesOrdens { get; set; }
        public virtual Vendedor Vendedor { get; set; }
    }
}