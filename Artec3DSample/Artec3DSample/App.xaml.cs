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
        public new NavigationPage MainPage
        {
            get => base.MainPage as NavigationPage;
            set => base.MainPage = value;
        }

        public static IContainer Container { get; private set; }

        public App(ContainerBuilder containerBuilder)
        {
            InitializeComponent();

            containerBuilder.RegisterType<NavigationService>().As<INavigationService>().WithParameter("rootPage", MainPage).SingleInstance(); //todo null??
            containerBuilder.RegisterType<SettingsProvider>().As<ISettingsProvider>().SingleInstance();

            containerBuilder.RegisterType<TaskListPageViewModel>();

            Container = containerBuilder.Build();

            MainPage = new NavigationPage(new TaskListPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
