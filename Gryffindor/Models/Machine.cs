using System;
using System.Collections.Generic;

namespace Gryffindor.Models
{
    public partial class Machine
    {
        public Machine()
        {
            PurchaseRequest = new HashSet<PurchaseRequest>();
        }

        public int MachineId { get; set; }
        public string MachineName { get; set; }
        public int? MachineQty { get; set; }
        public DateTime? MachineTime { get; set; }

        public ICollection<PurchaseRequest> PurchaseRequest { get; set; }
    }
}
