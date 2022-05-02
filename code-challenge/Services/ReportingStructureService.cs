using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using challenge.Repositories;

namespace challenge.Services
{
    public class ReportingStructureService : IReportingStructureService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<ReportingStructureService> _logger;

        public ReportingStructureService(ILogger<ReportingStructureService> logger, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public ReportingStructure GetReportingStructureById(string id)
        {
            if(!String.IsNullOrEmpty(id))
            {
                Employee employee = _employeeRepository.GetById(id);
                ReportingStructure reportingStructure = new ReportingStructure(employee, _employeeRepository);
                return reportingStructure;
            }

            return null;
        }
    }
}
