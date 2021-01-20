using Artec3DSample.Classes;
using Artec3DSample.Interfaces;
using Artec3DSample.Models.DAO;
using Artec3DSample.Models.DTO;
using Artec3DSample.Models.DTO.Enums;
using Artec3DSample.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Artec3DSample.ViewModels
{
    public class TaskListPageViewModel : BaseViewModel
    {
        private ObservableCollection<TaskModel> _tasks;
        public ObservableCollection<TaskModel> Tasks
        {
            get => _tasks;
            set
            {
                _tasks = value;
                OnPropertyChanged();
            }
        }

        public ICommand CreateNewTaskCommand { get; set; }
        public ICommand RemoveSelectedTasksCommand { get; set; }

        private readonly INavigationService _navigationService;
        private readonly ISettingsProvider _settingsProvider;

        public TaskListPageViewModel(INavigationService navigationService, ISettingsProvider settingsProvider) : base("Tasks", navigationService)
        {
            _navigationService = navigationService;
            _settingsProvider = settingsProvider;

            CreateNewTaskCommand = new Command(() => OperateTask(nameof(CreateNewTask)));
            RemoveSelectedTasksCommand = new Command(() => OperateTask(nameof(RemoveSelectedTasks)));

            Tasks = new ObservableCollection<TaskModel>();
        }

        public Task Refresh()
        {
            // For a real app I'd use an SQLite for such kind of data but for that sample I will use a simple data caching (SharedPreferences for Android and NSUserDefaults for iOS)
            var tasks = _settingsProvider.GetJsonValueOrDefault<List<TaskItem>>(SettingsProvider.Tasks);

            if (tasks == null)
            {
                var defaultTasks = new List<TaskItem>
                {
                    new TaskItem { Id = Guid.NewGuid(), CreatedAt = DateTime.Now.Subtract(TimeSpan.FromMinutes(30)), Description = "Brush teeth", Status = TaskItemStatus.Completed },
                    new TaskItem { Id = Guid.NewGuid(), CreatedAt = DateTime.Now, Description = "Wash the dishes", Status = TaskItemStatus.InProgress },
                    new TaskItem { Id = Guid.NewGuid(), CreatedAt = DateTime.Now.Add(TimeSpan.FromMinutes(25)), Description = "Take out the trash", Status = TaskItemStatus.Opened }
                };

                _settingsProvider.AddOrUpdateJsonValue(SettingsProvider.Tasks, defaultTasks);

                tasks = defaultTasks;
            }

            Tasks = new ObservableCollection<TaskModel>(tasks.OrderBy(t => t.CreatedAt).Select(t => new TaskModel(t)
            {
                EditTaskCommand = new Command(() => OperateTask(nameof(OpenTaskForEditing), t.Id))
            }));

            return Task.CompletedTask;
        }

        public Task OpenTaskForEditing(Guid taskId)
        {
            return _navigationService.PushAsync(nameof(EditTaskPage), true, taskId);
        }

        public Task CreateNewTask()
        {
            return _navigationService.PushAsync(nameof(EditTaskPage), true);
        }

        public Task RemoveSelectedTasks()
        {
            var tasks = Tasks.Where(t => !t.IsSelected).ToList();

            _settingsProvider.AddOrUpdateJsonValue(SettingsProvider.Tasks, tasks);

            return Task.CompletedTask;
        }
    }
}
