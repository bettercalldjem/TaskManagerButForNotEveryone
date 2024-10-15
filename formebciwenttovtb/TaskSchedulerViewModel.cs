using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskScheduler.Models;
using TaskScheduler.Services;
using TaskScheduler.ViewModels;

namespace formebciwenttovtb.ViewModels
{
    public class TaskSchedulerViewModel : BaseViewModel
    {
        private readonly IDataService _dataService;

        public ObservableCollection<TaskItem> Tasks { get; set; }
        public TaskStatistics Statistics { get; set; }

        private string _newTaskDescription;
        public string NewTaskDescription
        {
            get => _newTaskDescription;
            set
            {
                _newTaskDescription = value;
                OnPropertyChanged(nameof(NewTaskDescription));
            }
        }

        private DateTime _newTaskDeadline = DateTime.Now;
        public DateTime NewTaskDeadline
        {
            get => _newTaskDeadline;
            set
            {
                _newTaskDeadline = value;
                OnPropertyChanged(nameof(NewTaskDeadline));
            }
        }

        private int _newTaskPriority = 1;
        public int NewTaskPriority
        {
            get => _newTaskPriority;
            set
            {
                _newTaskPriority = value;
                OnPropertyChanged(nameof(NewTaskPriority));
            }
        }

        public TaskSchedulerViewModel()
        {
            _dataService = new FileDataService(); Tasks = new ObservableCollection<TaskItem>();
            Statistics = new TaskStatistics();

            LoadTasksAsync();

            StartStatisticsTimer();
        }

        public ICommand AddTaskCommand => new RelayCommand(AddTask);

        private void AddTask()
        {
            var newTask = new TaskItem
            {
                Description = NewTaskDescription,
                Priority = NewTaskPriority,
                Deadline = NewTaskDeadline,
                IsCompleted = false
            };

            Tasks.Add(newTask);

            NewTaskDescription = string.Empty;
            NewTaskDeadline = DateTime.Now;
            NewTaskPriority = 1;

            SaveTasksAsync();
        }

        public ICommand RemoveTaskCommand => new RelayCommand<TaskItem>(RemoveTask);

        private void RemoveTask(TaskItem task)
        {
            if (task != null)
            {
                Tasks.Remove(task);
                SaveTasksAsync();
                UpdateStatistics();
            }
        }

        private async void LoadTasksAsync()
        {
            var tasks = await _dataService.LoadTasksAsync();
            foreach (var task in tasks)
            {
                Tasks.Add(task);
            }

            UpdateStatistics();
        }

        public async Task SaveTasksAsync()
        {
            await _dataService.SaveTasksAsync(Tasks);
        }

        private void StartStatisticsTimer()
        {
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(30);
            timer.Tick += (s, e) => UpdateStatistics();
            timer.Start();
        }

        private void UpdateStatistics()
        {
            Statistics.TotalTasks = Tasks.Count;
            Statistics.CompletedTasks = Tasks.Count(t => t.IsCompleted);
            Statistics.AveragePriority = Tasks.Count > 0 ? Tasks.Average(t => t.Priority) : 0;
            Statistics.OverdueTasks = Tasks.Count(t => t.Deadline < DateTime.Now && !t.IsCompleted);
        }
    }
}
