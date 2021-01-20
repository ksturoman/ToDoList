using System.Threading.Tasks;
using Xamarin.Forms;

namespace Artec3DSample.Interfaces
{
    public interface INavigationService
    {
        void SetRootPage(NavigationPage rootPage);

        Task PushAsync(string pageKey, bool animated = false, params object[] pageArgs);

        Task PopAsync(bool animated = false);

        Task DisplayAlert(string title, string message, string accept = "Ok", string cancel = "Cancel");
    }
}
