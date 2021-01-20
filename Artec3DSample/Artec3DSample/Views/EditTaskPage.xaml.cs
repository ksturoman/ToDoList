using Artec3DSample.ViewModels;
using Autofac;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Artec3DSample.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditTaskPage : ContentPage
    {
        private EditTaskPageViewModel ViewModel => (EditTaskPageViewModel) BindingContext;

        public EditTaskPage() : this(default)
        {
            
        }

        public EditTaskPage(Guid? taskId)
        {
            InitializeComponent();

            BindingContext = App.Container.Resolve<EditTaskPageViewModel>(new NamedParameter("taskId", taskId));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ViewModel.OperateTask(nameof(EditTaskPageViewModel.Refresh));
        }
    }
}