using TaskScheduler.ViewModels;

namespace TaskScheduler.Models
{
    public class TaskStatistics : BaseViewModel
    {
        private int _totalTasks;
        public int TotalTasks
        {
            get => _totalTasks;
            set
            {
                _totalTasks = value;
                OnPropertyChanged(nameof(TotalTasks));
            }
        }

        private int _completedTasks;
        public int CompletedTasks
        {
            get => _completedTasks;
            set
            {
                _completedTasks = value;
                OnPropertyChanged(nameof(CompletedTasks));
            }
        }

        private double _averagePriority;
        public double AveragePriority
        {
            get => _averagePriority;
            set
            {
                _averagePriority = value;
                OnPropertyChanged(nameof(AveragePriority));
            }
        }

        private int _overdueTasks;
        public int OverdueTasks
        {
            get => _overdueTasks;
            set
            {
                _overdueTasks = value;
                OnPropertyChanged(nameof(OverdueTasks));
            }
        }
    }
}
