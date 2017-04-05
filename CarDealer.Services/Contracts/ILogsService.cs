using CarDealer.Models.ViewModels.Logs;

namespace CarDealer.Services
{
    public interface ILogsService
    {
        AllLogsPageViewModel GetAllLogsPage(string username, int? page);
        void DeleteAllLogs();
    }
}