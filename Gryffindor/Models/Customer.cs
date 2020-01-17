using System;
using System.Collections.Generic;

namespace Gryffindor.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Order = new HashSet<Order>();
            PurchaseRequest = new HashSet<PurchaseRequest>();
        }

        public int CustomerId { get; set; }
        public string CustomerCompany { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerTel { get; set; }

        public ICollection<Order> Order { get; set; }
        public ICollection<PurchaseRequest> PurchaseRequest { get; set; }
    }
}
