using System.ComponentModel;

namespace Mini.Settings
{
    public abstract class GlobalStateNotifier<T> : BindableBase
        where T : INotifyPropertyChanged
    {
    }
}
