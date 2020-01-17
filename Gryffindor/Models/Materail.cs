using System;
using System.Collections.Generic;

namespace Gryffindor.Models
{
    public partial class Materail
    {
        public Materail()
        {
            Product = new HashSet<Product>();
        }

        public int MaterailId { get; set; }
        public string MaterailName { get; set; }
        public string MaterailColor { get; set; }
        public int? MaterailQty { get; set; }

        public ICollection<Product> Product { get; set; }
    }
}
