using System.ComponentModel;
using UnityEngine;

using Mini.Settings.Utils;

namespace Mini.Settings.Examples
{
    public class AppSettings_GameObjectSelector : ConsumerMonoBehaviour<AppSettings>
    {
        [SerializeField]
        private bool _useAppSettings = true;
        [SerializeField]
        private bool _setActiveOnAwake = false;
        [SerializeField]
        private string appSettingsPropertyName;
        [SerializeField]
        private GameObject defaultGameObject;
        [SerializeField]
        private int selectedGameObjectIndex;
        private GameObject _selectedGameObject;
        public GameObject SelectedGameObject { get => _selectedGameObject ??= SelectGameObject() ?? defaultGameObject; }

        public SerializableDictionary<int, GameObject> ListGameObject = new SerializableDictionary<int, GameObject>();

        protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ParseAppSettings();
        }

        private void ParseAppSettings()
        {
            if (!_useAppSettings)
            {
                return;
            }

            var appSettings = Context;
            var propertyInfo = appSettings.GetType().GetProperty(appSettingsPropertyName);
            var property = propertyInfo.GetValue(appSettings, null);
            if (property != null)
            {
                selectedGameObjectIndex = (int)property;
            }
        }

        private void SetActiveOnAwake()
        {
            if (!_setActiveOnAwake)
            {
                return;
            }

            if (!ListGameObject.ContainsKey(selectedGameObjectIndex))
            {
                return;
            }

            foreach(var pairGameObject in ListGameObject)
            {
                if (pairGameObject.Key != selectedGameObjectIndex)
                {
                    pairGameObject.Value.SetActive(false);
                    continue;
                }
                pairGameObject.Value.SetActive(true);
            }
        }

        private GameObject SelectGameObject()
        {
            if (ListGameObject.ContainsKey(selectedGameObjectIndex))
            {
                return ListGameObject[selectedGameObjectIndex];
            }
            return null;
        }

        protected override void Awake()
        {
            base.Awake();

            ParseAppSettings();
            SetActiveOnAwake();
        }
    }
}
