using Artec3DSample.Classes;
using Artec3DSample.Models.DTO;
using Artec3DSample.Models.DTO.Enums;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Artec3DSample.Models.DAO
{
    public class TaskModel : PropertyChangedListener
    {
        private Guid _id;
        public Guid Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _createdAt;
        public DateTime? CreatedAt
        {
            get => _createdAt;
            set
            {
                _createdAt = value;
                OnPropertyChanged();
            }
        }

        public Color StatusColor => Status.TaskStatus.ToColor();

        public string StatusString => Status.ToString();

        private TaskStatusModel _status;
        public TaskStatusModel Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(StatusColor));
                OnPropertyChanged(nameof(StatusString));
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        public ICommand EditTaskCommand { get; set; }

        public TaskModel()
        {

        }

        public TaskModel(TaskItem taskItem)
        {
            Id = taskItem.Id;
            CreatedAt = taskItem.CreatedAt;
            Status = new TaskStatusModel(taskItem.Status);
            Description = taskItem.Description;
        }

        public TaskItem BuildTaskItem()
        {
            return new TaskItem
            {
                Id = Id,
                CreatedAt = CreatedAt,
                Description = Description,
                Status = Status.TaskStatus
            };
        }
    }
}
