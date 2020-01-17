using System;
using System.Collections.Generic;

namespace Gryffindor.Models
{
    public partial class DateYearoff
    {
        public DateYearoff()
        {
            PurchaseRequest = new HashSet<PurchaseRequest>();
        }

        public int DateId { get; set; }
        public DateTime? DateDateoff { get; set; }

        public ICollection<PurchaseRequest> PurchaseRequest { get; set; }
    }
}
