using System;
using System.Collections.Generic;

namespace Gryffindor.Models
{
    public partial class PurchaseRequest
    {
        public PurchaseRequest()
        {
            SchedulePlan = new HashSet<SchedulePlan>();
        }

        public int PurchaseId { get; set; }
        public DateTime? PhurchseDoc { get; set; }
        public DateTime? PhurchaseDate { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerCompany { get; set; }
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public int? MaterailId { get; set; }
        public string MaterailName { get; set; }
        public int? OrderId { get; set; }
        public int? OrderQty { get; set; }
        public int? DateId { get; set; }
        public DateTime? DateDateoff { get; set; }
        public int? MonthId { get; set; }
        public string MonthName { get; set; }
        public int? StatusId { get; set; }
        public string StatusName { get; set; }
        public int? Delivery { get; set; }
        public int? PurchaseTotal { get; set; }
        public int? MachineId { get; set; }
        public string MachineName { get; set; }
        public DateTime? MachineTime { get; set; }
        public int? IssueId { get; set; }
        public string IssueName { get; set; }

        public Customer Customer { get; set; }
        public DateYearoff Date { get; set; }
        public Issue Issue { get; set; }
        public Machine Machine { get; set; }
        public Order Order { get; set; }
        public ProductStatus Status { get; set; }
        public ICollection<SchedulePlan> SchedulePlan { get; set; }
    }
}
