using CodeChallenge.Models;
using CodeChallenge.Repositories;
using CodeChallenge.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/compensation")]
    public class CompensationController: ControllerBase
    {
        private readonly ICompensationService _compensationService;
        private readonly ILogger<CompensationController> _logger;
        public CompensationController(ICompensationService employeeRepository, ILogger<CompensationController> logger)
        {
            _compensationService = employeeRepository;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public IActionResult GetCompensation(String id)
        {
            _logger.LogDebug($"Recived Request to retrieve Compensation for EmployeeId: {id}");
            
            var compensation = _compensationService.GetById(id);
           
            if(compensation == null)
            {
                _logger.LogError($"Unable to find Compensation record for EmployeeId: {id}");
                return BadRequest("Unable to process request. Request data is invalid");
            }
            else
            {
                _logger.LogDebug($"Sucessfully Process Compensation Request for EmployeeId: {id}");
                return Ok(compensation);
            }
            
            
        }
        [HttpPost]
        public IActionResult CreateCompensationRecord([FromBody] Compensation compensation)
        {
            _logger.LogDebug($"Creating compensation record for EmployeeId: {compensation?.Employee}");
            
            var result = _compensationService.Create(compensation);

            if(result == null)
            {
                _logger.LogError($"Unsuccesfully request to create compensation record for EmployeeId: {compensation?.Employee}");
                return BadRequest("Unable to process request. Request data is invalid");
            }
            else 
            {
                _logger.LogDebug($"Succesfully created compensation record for EmployeeId: {compensation?.Employee}");
                return Created("/api/compensation",compensation);
            }
            

        }

    }
}
