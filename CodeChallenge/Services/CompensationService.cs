using CodeChallenge.Models;
using CodeChallenge.Repositories.Interfaces;
using CodeChallenge.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Services
{
    public class CompensationService: ICompensationService
    {
        private readonly ICompensationRepository _compensationRepository;
        private readonly ILogger _logger;
        public CompensationService(ILogger<CompensationService> logger, ICompensationRepository compensationRepository) 
        {
            _logger = logger;
            _compensationRepository = compensationRepository;
        }

        public Compensation Create(Compensation compensation)
        {
            if (compensation == null)
            {
                _logger.LogError("Unable to process argument due to bad request data");
                return null;
            }

            _compensationRepository.Create(compensation);
            _compensationRepository.SaveAsync().Wait();

            return compensation;
        }

        public Compensation GetById(string id) 
        {
            if(!string.IsNullOrEmpty(id))
            {
                return _compensationRepository.GetById(id);
            }

            return null;
        }

    }
}
