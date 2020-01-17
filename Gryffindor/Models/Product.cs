using System;
using System.Collections.Generic;

namespace Gryffindor.Models
{
    public partial class Product
    {
        public Product()
        {
            Order = new HashSet<Order>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductSize { get; set; }
        public string ProductColor { get; set; }
        public int? MaterailId { get; set; }
        public string MarerailName { get; set; }

        public Materail Materail { get; set; }
        public ICollection<Order> Order { get; set; }
    }
}
