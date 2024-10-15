using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TaskScheduler.Models;

namespace TaskScheduler.Services
{
    public class FileDataService : IDataService
    {
        private const string FilePath = "tasks.json";

        public async Task<List<TaskItem>> LoadTasksAsync()
        {
            if (!File.Exists(FilePath))
                return new List<TaskItem>();

            var json = await File.ReadAllTextAsync(FilePath);
            return JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
        }

        public async Task SaveTasksAsync(IEnumerable<TaskItem> tasks)
        {
            var json = JsonSerializer.Serialize(tasks);
            await File.WriteAllTextAsync(FilePath, json);
        }
    }
}
