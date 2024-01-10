using CodeChallenge.Models;

namespace CodeChallenge.Services.Interfaces
{
    public interface IReportingService
    {
        ReportingStructure GetNumberOfReports(string id);
    }
}
