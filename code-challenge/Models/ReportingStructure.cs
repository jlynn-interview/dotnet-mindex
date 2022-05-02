using challenge.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Models
{
    public class ReportingStructure
    {
        public Employee Employee;
        public int NumberOfReports;

        public ReportingStructure(Employee employee, IEmployeeRepository employeeRepository)
        {
            this.Employee = employee;
            this.NumberOfReports = getNumberOfReports(employee, employeeRepository);
        }

		//Generates the number of reporting employees to reporting structure's employee
		public int getNumberOfReports(Employee employee, IEmployeeRepository employeeRepository)
		{
			List<Employee> reporters = employee.DirectReports;
			List<Employee> totalReporters = new List<Employee>();

			//traverse all reporters and their reporters, ignore duplicates
			while (reporters != null && reporters.Count != 0)
			{
				Employee currReporter = employeeRepository.GetById(reporters.First().EmployeeId);
				List<Employee> currReporterReports = currReporter.DirectReports;
				if (currReporterReports != null)
				{
					foreach (Employee e in currReporterReports)
					{
						if (!totalReporters.Contains(e))
						{
							totalReporters.Add(e);
							reporters.Add(e);
						}
					}
				}
				if (!totalReporters.Contains(currReporter))
				{
					totalReporters.Add(currReporter);
				}
				reporters.Remove(reporters.First());
			}

			int numberOfReports = totalReporters.Count();
			return numberOfReports;
		}
	}
}
