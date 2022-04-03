using System.ComponentModel;

namespace Mini.AppSettings
{
    public abstract class GlobalStateNotifier<T> : BindableBase
        where T : INotifyPropertyChanged
    {
    }
}
