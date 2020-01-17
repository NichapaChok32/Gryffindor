using System;
using System.Collections.Generic;

namespace Gryffindor.Models
{
    public partial class DateMount
    {
        public DateMount()
        {
            SchedulePlan = new HashSet<SchedulePlan>();
        }

        public int MonthId { get; set; }
        public string MonthName { get; set; }

        public ICollection<SchedulePlan> SchedulePlan { get; set; }
    }
}
