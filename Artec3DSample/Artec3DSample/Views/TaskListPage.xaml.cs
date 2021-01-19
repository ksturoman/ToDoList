using Artec3DSample.ViewModels;
using Autofac;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Artec3DSample.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskListPage : ContentPage
    {
        private TaskListPageViewModel ViewModel => (TaskListPageViewModel) BindingContext;

        public TaskListPage()
        {
            InitializeComponent();

            BindingContext = App.Container.Resolve<TaskListPageViewModel>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ViewModel.OperateTask(nameof(TaskListPageViewModel.Refresh));
        }
    }
}