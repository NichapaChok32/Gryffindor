using System;
using System.Collections.Generic;

namespace Gryffindor.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Order = new HashSet<Order>();
        }

        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeePosition { get; set; }
        public double? EmployeeSaraly { get; set; }
        public string EmployeeSex { get; set; }
        public string EmployeeDepartment { get; set; }

        public ICollection<Order> Order { get; set; }
    }
}
