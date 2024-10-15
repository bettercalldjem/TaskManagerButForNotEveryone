using System.Collections.Generic;
using System.Threading.Tasks;
using TaskScheduler.Models;

namespace TaskScheduler.Services
{
    public interface IDataService
    {
        Task<List<TaskItem>> LoadTasksAsync();
        Task SaveTasksAsync(IEnumerable<TaskItem> tasks);
    }
}
