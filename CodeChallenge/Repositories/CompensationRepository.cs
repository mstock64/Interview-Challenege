using CodeChallenge.Data;
using CodeChallenge.Models;
using CodeChallenge.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Repositories
{
    public class CompensationRepository: ICompensationRepository
    {
        private EmployeeContext _employeeContext;
        private readonly ILogger<CompensationRepository> _logger;

        public CompensationRepository(EmployeeContext employeeContext, ILogger<CompensationRepository> logger)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        public Compensation GetById(String id)
        {
            return _employeeContext.Compensations.FirstOrDefault(e => e.Employee == id);
        }

        public void Create(Compensation compensation)
        {
            _employeeContext.Compensations.Add(compensation);
        }

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }

    }
}
