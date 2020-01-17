using System;
using System.Collections.Generic;

namespace Gryffindor.Models
{
    public partial class ProductStatus
    {
        public ProductStatus()
        {
            PurchaseRequest = new HashSet<PurchaseRequest>();
        }

        public int StatusId { get; set; }
        public string StatusName { get; set; }

        public ICollection<PurchaseRequest> PurchaseRequest { get; set; }
    }
}
