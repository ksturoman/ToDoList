using Artec3DSample.ViewModels;
using System;

namespace Artec3DSample.Classes.Exceptions
{
    public class NotSupportedMethodException : Exception
    {
        public NotSupportedMethodException(string name, BaseViewModel viewModel) : base($"{name} not supported by {viewModel.GetType().Name}")
        {

        }
    }
}
