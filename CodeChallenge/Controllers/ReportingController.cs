using CodeChallenge.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace CodeChallenge.Controllers
{
    [Route("api/reports")]
    [ApiController]
    public class ReportingController: ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IReportingService _reportingService;
        public ReportingController(ILogger<ReportingController> logger, IReportingService reportingService)
        {
            _logger = logger;
            _reportingService = reportingService;
        }

        [HttpGet("{id}")]
        public IActionResult GetNumberOfReports(String id)
        {
            _logger.LogDebug($"Received GET request for number of employee reports -- EmployeeId:'{id}'");

            var numberOfReports = _reportingService.GetNumberOfReports(id);

            if (numberOfReports == null)
                return BadRequest("Invalid data provided");

            _logger.LogDebug($"Successfully Served GET request for number of employee reports -- EmployeeId '{id}'");

            return Ok(numberOfReports);
        }
    }
}

