using System;
using System.Collections.Generic;

namespace Gryffindor.Models
{
    public partial class Order
    {
        public Order()
        {
            PurchaseRequest = new HashSet<PurchaseRequest>();
        }

        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? OrderQty { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerCompany { get; set; }
        public int? MaterailId { get; set; }
        public string MaterailName { get; set; }
        public string MaterailColor { get; set; }
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public int? ProductSize { get; set; }
        public string ProductColor { get; set; }
        public int? EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeDepartment { get; set; }

        public Customer Customer { get; set; }
        public Employee Employee { get; set; }
        public Product Product { get; set; }
        public ICollection<PurchaseRequest> PurchaseRequest { get; set; }
    }
}
