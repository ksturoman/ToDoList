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

        public Color StatusColor => Status.ToColor();

        public string StatusString => Status.ToString();

        private TaskItemStatus _status;
        public TaskItemStatus Status
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

        public Color SelectionColor => IsSelected ? Color.FromHex("#cfd8dc") : Color.Transparent;

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectionColor));
            }
        }

        public ICommand SelectedCommand { get; }

        public ICommand EditTaskCommand { get; set; }

        public TaskModel()
        {
            SelectedCommand = new Command(() => { IsSelected = !IsSelected; }); //todo temp
        }

        public TaskModel(TaskItem taskItem) : this()
        {
            Id = taskItem.Id;
            CreatedAt = taskItem.CreatedAt;
            Status = taskItem.Status;
            Description = taskItem.Description;
        }

        public TaskItem BuildTaskItem()
        {
            return new TaskItem
            {
                Id = Id,
                CreatedAt = CreatedAt,
                Description = Description,
                Status = Status
            };
        }
    }
}
