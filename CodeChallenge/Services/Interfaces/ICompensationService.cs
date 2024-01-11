using CodeChallenge.Models;

namespace CodeChallenge.Services.Interfaces
{
    public interface ICompensationService
    {
        Compensation Create(Compensation compensation);

        Compensation GetById(string id);
    }
}
