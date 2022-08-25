using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

namespace Mini.Settings.Examples
{
    public class AppSettings_BooleanSelector : ConsumerMonoBehaviour<AppSettings>
    {
        [SerializeField]
        private bool _useAppSettings = true;
        [SerializeField]
        private string _propertyName;

        public UnityEvent PropertyTrueEvent = new UnityEvent();
        public UnityEvent PropertyFalseEvent = new UnityEvent();

        private bool _isChanged = false;
        private bool? _propertyValue;

        protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ParseAppSettings();
            NotifyEvent();
        }

        private void ParseAppSettings()
        {
            if (!_useAppSettings)
            {
                return;
            }

            var appSettings = Context;
            var propertyInfo = appSettings.GetType().GetProperty(_propertyName);
            var property = propertyInfo.GetValue(appSettings, null);
            if (property != null)
            {
                _propertyValue = (bool)property;
                _isChanged = true;
            }
        }

        private void NotifyEvent()
        {
            if (_propertyValue == null)
            {
                return;
            }
            if (!_isChanged)
            {
                return;
            }

            ((bool)_propertyValue ? PropertyTrueEvent : PropertyFalseEvent)?.Invoke();
        }
    }
}
