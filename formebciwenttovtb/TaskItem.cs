using System;
using TaskScheduler.ViewModels;

namespace TaskScheduler.Models
{
    public class TaskItem : BaseViewModel
    {
        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private int _priority;
        public int Priority
        {
            get => _priority;
            set
            {
                _priority = value;
                OnPropertyChanged(nameof(Priority));
            }
        }

        private DateTime _deadline;
        public DateTime Deadline
        {
            get => _deadline;
            set
            {
                _deadline = value;
                OnPropertyChanged(nameof(Deadline));
            }
        }

        public bool IsCompleted { get; set; }
    }
}
