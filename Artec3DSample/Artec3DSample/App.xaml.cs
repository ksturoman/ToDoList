using Artec3DSample.Classes;
using Artec3DSample.Interfaces;
using Artec3DSample.ViewModels;
using Artec3DSample.Views;
using Autofac;
using Xamarin.Forms;

namespace Artec3DSample
{
    public partial class App : Application
    {
        public static IContainer Container { get; private set; }

        public App(ContainerBuilder containerBuilder)
        {
            InitializeComponent();

            containerBuilder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            containerBuilder.RegisterType<SettingsProvider>().As<ISettingsProvider>().SingleInstance();

            containerBuilder.RegisterType<TaskListPageViewModel>();
            containerBuilder.RegisterType<EditTaskPageViewModel>();

            Container = containerBuilder.Build();

            MainPage = new NavigationPage(new TaskListPage());

            Container.Resolve<INavigationService>().SetRootPage((NavigationPage) MainPage);
        }
    }
}
