using System.Collections.Generic;
using System.Linq;
using CarDealer.Models.DbModels;
using CarDealer.Models.ViewModels.Logs;

namespace CarDealer.Services
{
  public class LogsService : Service, ILogsService
  {
        public AllLogsPageViewModel GetAllLogsPage(string username, int? page)
        {
            int currentPage = 1;
            if (page != null)
            {
                currentPage = page.Value;
            }
            IEnumerable<Log> logs;

            if (username != null)
            {
                logs = this.Context.Logs.Where(log => log.User.Username == username);
            }
            else
            {
                logs = this.Context.Logs;
            }

            int allLogPagesCount = logs.Count() /20 + (logs.Count() % 20 == 0 ? 0 : 1);

            int logsTotake = 20;

            if (allLogPagesCount == currentPage)
            {
                logsTotake = logs.Count() % 20 == 0 ? 20 : logs.Count() % 20;
            }
            logs = logs.Skip((currentPage - 1) * 20).Take(logsTotake);
            List<AllLogsViewModel> logVms = new List<AllLogsViewModel>();

            foreach (Log log in logs)
            {
                logVms.Add(new AllLogsViewModel()
                {
                    Operation = log.Operation,
                    ModifiedTable = log.ModifiedTableName,
                    UserName = log.User.Username,
                    ModifiedAt = log.ModifiedAt
                });
              
            }

            AllLogsPageViewModel pageVm = new AllLogsPageViewModel()
            {
                WantedUserName = username,
                CurrentPage = currentPage,
                TotalNumberOfPages = allLogPagesCount,
                Logs = logVms
            };
            return pageVm;
        }

        public void DeleteAllLogs()
        {
            this.Context.Logs.RemoveRange(this.Context.Logs);
            this.Context.SaveChanges();
        }
    }
}
