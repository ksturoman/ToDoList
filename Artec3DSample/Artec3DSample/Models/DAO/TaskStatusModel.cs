using Artec3DSample.Classes;
using Artec3DSample.Models.DTO.Enums;
using System;

namespace Artec3DSample.Models.DAO
{
    public class TaskStatusModel : PropertyChangedListener, IEquatable<TaskStatusModel>
    {
        private TaskItemStatus _taskStatus;
        public TaskItemStatus TaskStatus
        {
            get => _taskStatus;
            set
            {
                _taskStatus = value;
                OnPropertyChanged();
            }
        }

        public TaskStatusModel(TaskItemStatus taskItemStatus)
        {
            TaskStatus = taskItemStatus;
        }

        public bool Equals(TaskStatusModel other) => other != null && other.TaskStatus == TaskStatus;

        public override string ToString() => TaskStatus.ToUserFriendlyString();
    }
}
