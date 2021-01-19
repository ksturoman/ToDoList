using Artec3DSample.Classes;
using Artec3DSample.Classes.Exceptions;
using Artec3DSample.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Artec3DSample.ViewModels
{
    public class BaseViewModel : PropertyChangedListener
    {
        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public string PageName { get; set; }

        private readonly INavigationService _navigationService;

        public BaseViewModel(string pageName, INavigationService navigationService)
        {
            _navigationService = navigationService;

            PageName = pageName;
        }

        public async void OperateTask(string name, params object[] parameters)
        {
            if (IsLoading)
            {
                return;
            }

            try
            {
                IsLoading = true;

                if (GetType().GetMethods().FirstOrDefault(m => m.Name == name) is { } method)
                {
                    await (Task) GetType().GetMethods().First(m => m.Name == name).Invoke(this, parameters);
                }
                else
                {
                    throw new NotSupportedMethodException(name, this);
                }

            }
            catch (Exception e)
            {
                await _navigationService.DisplayAlert(PageName, e.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
