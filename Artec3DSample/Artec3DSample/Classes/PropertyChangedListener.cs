using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Artec3DSample.Classes
{
    public class PropertyChangedListener : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
