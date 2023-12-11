using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shoeyStore.Models.ViewModels
{
    //This controller will be used to load the product navigation card list according to the filters
    public class ItemNavigationViewModel
    {
        public IEnumerable<Producto> Products { get; set; }
        public IEnumerable<string> Genders { get; set; }
        public IEnumerable<string> Brands { get; set; }
        public IEnumerable<string> Sizes { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
    }
}