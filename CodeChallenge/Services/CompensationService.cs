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

        /// <summary>
        /// Create and Stores new Compensation records
        /// </summary>
        /// <param name="compensation"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Retrieves Conpensation from Repository Layer using the id as key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
