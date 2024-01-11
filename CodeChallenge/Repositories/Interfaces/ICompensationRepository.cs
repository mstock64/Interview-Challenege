using CodeChallenge.Models;
using System.Threading.Tasks;

namespace CodeChallenge.Repositories.Interfaces
{
    public interface ICompensationRepository
    {
        Compensation GetById(string id);
        void Create(Compensation compensaition);

        Task SaveAsync();
    }
}
