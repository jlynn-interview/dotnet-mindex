using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using challenge.Services;
using challenge.Models;

namespace challenge.Controllers
{
    [Route("api/reportingstructure")]
    public class ReportingStructureController : Controller
    {
        private readonly IReportingStructureService _reportingStructureService;

        public ReportingStructureController(IReportingStructureService reportingStructureService)
        {
            _reportingStructureService = reportingStructureService;
        }

        [HttpGet("{id}", Name = "getReportingStructureById")]
        public IActionResult GetReportingStructureById(String id)
        {
            var reportingStructure = _reportingStructureService.GetReportingStructureById(id);

            if (reportingStructure == null)
                return NotFound();

            return Ok(reportingStructure);
        }
    }
}
