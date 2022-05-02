using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Models
{
    public class Compensation
    {
        public Compensation()
        {
        }

        public Compensation(Employee employee, decimal salary, DateTime effectiveDate)
        {
            this.Employee = employee;
            this.EmployeeId = employee.EmployeeId;
            this.Salary = salary;
            this.EffectiveDate = effectiveDate;
        }

        [Key]
        public String EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public decimal Salary { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
