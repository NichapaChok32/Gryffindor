using System;
using System.Collections.Generic;

namespace Gryffindor.Models
{
    public partial class SchedulePlan
    {
        public int ScheduleId { get; set; }
        public int? PurchaseId { get; set; }
        public int? MonthId { get; set; }
        public string MonthName { get; set; }
        public int SheduleTotal { get; set; }

        public DateMount Month { get; set; }
        public PurchaseRequest Purchase { get; set; }
    }
}
