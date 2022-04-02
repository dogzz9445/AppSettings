using System.ComponentModel;
using UnityEngine;

namespace Mini.AppSettings
{
    public abstract class ConsumerMonoBehaviour<T, S> : MonoBehaviour, INotifyPropertyChanged
        where S : INotifyPropertyChanged
        where T : GlobalStateNotifier<S>
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void Awake()
        {
            GlobalStateNotifier<S>.Global.PropertyChanged += PropertyChanged;
        }

        protected virtual void Start()
        {
            this.PropertyChanged += OnPropertyChanged;
            OnPropertyChanged(this, new PropertyChangedEventArgs("Start"));
        }

        protected virtual void OnDestroy()
        {
            GlobalStateNotifier<S>.Global.PropertyChanged -= PropertyChanged;
        }

        protected abstract void OnPropertyChanged(object sender, PropertyChangedEventArgs e);
    }
}
