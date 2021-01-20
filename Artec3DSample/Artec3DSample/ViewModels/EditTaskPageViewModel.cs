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
        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        private TaskModel _taskModel;
        public TaskModel TaskModel
        {
            get => _taskModel;
            set
            {
                _taskModel = value;
                OnPropertyChanged();
            }
        }

        public TaskItemStatus[] TaskStatuses => Enum.GetValues(typeof(TaskItemStatus)).Cast<TaskItemStatus>().ToArray(); //todo to model

        public ICommand SaveCommand { get; set; }

        private readonly Guid? _taskId;
        private readonly INavigationService _navigationService;
        private readonly ISettingsProvider _settingsProvider;

        public EditTaskPageViewModel(Guid? taskId, INavigationService navigationService, ISettingsProvider settingsProvider) : base("Task", navigationService)
        {
            _taskId = taskId;
            _navigationService = navigationService;
            _settingsProvider = settingsProvider;

            SaveCommand = new Command(() => OperateTask(nameof(Save)));
        }

        public Task Refresh()
        {
            if (_taskId.HasValue)
            {
                var tasks = _settingsProvider.GetJsonValueOrDefault<List<TaskItem>>(SettingsProvider.Tasks);

                TaskModel = new TaskModel(tasks.First(t => t.Id == _taskId));

                Title = "Edit task";
            }
            else
            {
                TaskModel = new TaskModel();

                Title = "Create task";
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

            tasks.Add(TaskModel.BuildTaskItem()); //todo create time

            await _navigationService.PopAsync(true);
        }
    }
}
