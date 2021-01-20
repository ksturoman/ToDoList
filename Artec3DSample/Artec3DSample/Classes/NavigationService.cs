using Artec3DSample.Interfaces;
using Artec3DSample.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Artec3DSample.Classes
{
    public class NavigationService : INavigationService
    {
        private NavigationPage _rootPage;

        private readonly Dictionary<string, Type> _pagesByKey;

        public NavigationService()
        {
            _pagesByKey = new Dictionary<string, Type>
            {
                {nameof(TaskListPage), typeof(TaskListPage)},
                {nameof(EditTaskPage), typeof(EditTaskPage)}
            };
        }

        public void SetRootPage(NavigationPage rootPage)
        {
            _rootPage = rootPage;
        }

        private static ConstructorInfo GetConstructor(Type type, IReadOnlyCollection<object> pageConstructorParameters)
        {
            ConstructorInfo constructor;

            if (pageConstructorParameters == null)
            {
                constructor = type.GetTypeInfo()
                    .DeclaredConstructors
                    .FirstOrDefault(c => !c.GetParameters().Any());
            }
            else
            {
                constructor = type.GetTypeInfo()
                    .DeclaredConstructors
                    .FirstOrDefault(c => c.GetParameters().Length == pageConstructorParameters.Count);
            }

            if (constructor == null)
            {
                throw new InvalidOperationException($"No suitable constructor found for type {type.Name}");
            }

            return constructor;
        }

        public async Task PushAsync(string pageKey, bool animated = false, params object[] pageArgs)
        {
            ConstructorInfo constructor;

            lock (_pagesByKey)
            {
                if (_pagesByKey.ContainsKey(pageKey))
                {
                    constructor = GetConstructor(_pagesByKey[pageKey], pageArgs);
                }
                else
                {
                    throw new ArgumentException($"Page with key \"{pageKey}\" not found");
                }
            }

            var page = constructor.Invoke(pageArgs ?? new object[] { }) as Page;

            await _rootPage.PushAsync(page, animated);
        }

        public Task PopAsync(bool animated = false)
        {
            return _rootPage.PopAsync(animated);
        }

        public Task DisplayAlert(string title, string message, string accept, string cancel)
        {
            return _rootPage.DisplayAlert(title, message, accept, cancel);
        }
    }
}
