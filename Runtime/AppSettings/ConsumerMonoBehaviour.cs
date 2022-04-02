using System.ComponentModel;
using UnityEngine;

namespace Mini.AppSettings
{
    public abstract class ConsumerMonoBehaviour<T> : MonoBehaviour, INotifyPropertyChanged
        where T : class, INotifyPropertyChanged, new()
    {
        protected virtual void Awake()
        {
            Provider<T>.Global.PropertyChanged += PropertyChanged;
        }

        protected virtual void Start()
        {
            this.PropertyChanged += OnPropertyChanged;
            OnPropertyChanged(this, new PropertyChangedEventArgs("Start"));
        }

        protected virtual void OnDestroy()
        {
            Provider<T>.Global.PropertyChanged -= PropertyChanged;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected abstract void OnPropertyChanged(object sender, PropertyChangedEventArgs e);
    }
}
