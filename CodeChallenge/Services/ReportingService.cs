using CodeChallenge.Models;
using CodeChallenge.Repositories;
using CodeChallenge.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Services
{
    public class ReportingService : IReportingService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<ReportingService> _logger;

        public ReportingService(IEmployeeRepository employeeRepository, ILogger<ReportingService> logger)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        /// <summary>
        /// Handles Error Handling for grabbing an Employee Record and filling in 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ReportingStructure GetNumberOfReports(string id)
        {
            var employee = _employeeRepository.GetById(id);

            if (employee == null)
            {
                _logger.LogError($"Unable to find Employee for id: {id}");
                return null;
            }

            return new ReportingStructure()
            {
                Employee = employee,
                NumberOfReports = FindTotalNumberOfReports(id)
            };

        }
        /// <summary>
        /// Finds the total number of employee reports given an employeeId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private int FindTotalNumberOfReports(string id)
        {
            var employee = _employeeRepository.GetById(id);
            var sum = 0;
            if (employee.DirectReports != null && employee.DirectReports.Count > 0)
            {
                foreach (var emp in employee.DirectReports)
                {
                    sum += FindTotalNumberOfReports(emp.EmployeeId) + 1;
                }
            }

            return sum;
        }
    }
}
