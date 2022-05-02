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
    [Route("api/compensation")]
    public class CompensationController : Controller
    {
        private readonly ICompensationService _compensationService;

        public CompensationController(ICompensationService compensationService)
        {
            _compensationService = compensationService;
        }

        [HttpPost]
        public IActionResult CreateCompensation([FromBody] Compensation compensation)
        {
            _compensationService.Create(compensation);
            CreatedAtRouteResult returnable = CreatedAtRoute("getCompensationById", new { id = compensation.Employee.EmployeeId }, compensation);

            return returnable;
        }

        [HttpGet("{id}", Name = "getCompensationById")]
        public IActionResult GetCompensationById(String id)
        {
            var compensation = _compensationService.GetById(id);

            if (compensation == null)
                return NotFound();

            return Ok(compensation);
        }
    }
}
