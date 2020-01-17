using System;
using System.Collections.Generic;

namespace Gryffindor.Models
{
    public partial class Issue
    {
        public Issue()
        {
            PurchaseRequest = new HashSet<PurchaseRequest>();
        }

        public int IssueId { get; set; }
        public string IssueName { get; set; }
        public string IssueStatus { get; set; }
        public string EmployeeName { get; set; }

        public ICollection<PurchaseRequest> PurchaseRequest { get; set; }
    }
}
