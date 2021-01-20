using Artec3DSample.Classes;
using Artec3DSample.Interfaces;
using Artec3DSample.Models.DAO;
using Artec3DSample.Models.DTO;
using Artec3DSample.Models.DTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Artec3DSample.ViewModels
{
    public class EditTaskPageViewModel : BaseViewModel
    {
        public string Title => IsExistingTask ? "Edit task" : "Create new task";

        public bool IsExistingTask => _taskId.HasValue;

        private TaskModel _taskModel;
        public TaskModel TaskModel
        {
            get => _taskModel;
            set
            {
                _taskModel = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsExistingTask));
                OnPropertyChanged(nameof(Title));
            }
        }

        public TaskStatusModel[] TaskStatuses => Enum.GetValues(typeof(TaskItemStatus)).Cast<TaskItemStatus>().Select(t => new TaskStatusModel(t)).ToArray();

        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        private readonly Guid? _taskId;
        private readonly INavigationService _navigationService;
        private readonly ISettingsProvider _settingsProvider;

        public EditTaskPageViewModel(Guid? taskId, INavigationService navigationService, ISettingsProvider settingsProvider) : base("Task", navigationService)
        {
            _taskId = taskId;
            _navigationService = navigationService;
            _settingsProvider = settingsProvider;

            SaveCommand = new Command(() => OperateTask(nameof(Save)));
            DeleteCommand = new Command(() => OperateTask(nameof(Delete)));
        }

        public Task Refresh()
        {
            if (_taskId.HasValue)
            {
                var tasks = _settingsProvider.GetJsonValueOrDefault<List<TaskItem>>(SettingsProvider.Tasks);

                TaskModel = new TaskModel(tasks.First(t => t.Id == _taskId));
            }
            else
            {
                TaskModel = new TaskModel();
            }

            return Task.CompletedTask;
        }

        public async Task Save()
        {
            var tasks = _settingsProvider.GetJsonValueOrDefault<List<TaskItem>>(SettingsProvider.Tasks);

            if (tasks.FirstOrDefault(t => t.Id == TaskModel.Id) is { } existingTask)
            {
                tasks.Remove(existingTask);
            }

            if (!IsExistingTask)
            {
                TaskModel.CreatedAt = DateTime.Now;
            }

            tasks.Add(TaskModel.BuildTaskItem());

            _settingsProvider.AddOrUpdateJsonValue(SettingsProvider.Tasks, tasks);

            await _navigationService.PopAsync(true);
        }

        public async Task Delete()
        {
            var tasks = _settingsProvider.GetJsonValueOrDefault<List<TaskItem>>(SettingsProvider.Tasks);

            var existingTask = tasks.First(t => t.Id == TaskModel.Id);

            tasks.Remove(existingTask);

            _settingsProvider.AddOrUpdateJsonValue(SettingsProvider.Tasks, tasks);

            await _navigationService.PopAsync(true);
        }
    }
}
