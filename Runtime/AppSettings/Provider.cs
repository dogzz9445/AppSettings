using System.ComponentModel;
using System.Net;
using System.IO;
using System;
using System.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;

#nullable enable
namespace Mini.AppSettings
{
    public class Provider<T> : GlobalStateNotifier<T>
        where T : class, INotifyPropertyChanged, new()
    {
        private static Provider<T> _global = null;
        public static new Provider<T> Global
        {
            get
            {
                if (_global == null)
                {
                    _global = new Provider<T>();
                }
                return _global;
            }
        }

        private T _appSettings;
        public T AppSettings 
        { 
            get 
            {
                if (_appSettings == null)
                {
                    Load();
                }
                return _appSettings;
            }
            set 
            {
                SetObservableProperty(ref _appSettings, value);
            }
        }

        private string _appSettingsFilePath;
        private string _appSettingsFileName;
        public string AppSettingsFullFileName { get => Path.Combine(_appSettingsFilePath, _appSettingsFileName); }
        
        private bool _isAutoSave;
        public bool IsAutoSave { get => _isAutoSave; set => _isAutoSave = value; }

        public Provider(
            string? filepath = null, 
            string? filename = null)
        {
            _appSettingsFilePath = filepath ?? Application.dataPath;
            _appSettingsFileName = filename ?? "app_settings.json";

            IsAutoSave = true;

            Load();

            this.PropertyChanged += OnPropertyChanged; 
        }

        public void Load()
        {
            if (!File.Exists(AppSettingsFullFileName))
            {
                AppSettings = new T();
            }
            else
            {
                AppSettings = ReadFile<T>(AppSettingsFullFileName);
            }
        }

        public async void Save()
        {
            await WriteFileAsync(AppSettingsFullFileName, AppSettings);
        }

        private void AutoSave()
        {
            if (IsAutoSave)
            {
                Save();
            }
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs eventArgs)
        {
            AutoSave();
        }

        public A ReadFile<A>(string filename) where A : class
        {
            if (File.Exists(filename))
            {
                var json = File.ReadAllText(filename);
                if (!string.IsNullOrEmpty(json))
                {
                    return JsonConvert.DeserializeObject<A>(json);
                }
            }
            return null;
        }

        public async Task WriteFileAsync<A>(string fullFileName, A jsonObject)
        {
            var directoryName = Path.GetDirectoryName(fullFileName);
            if (!string.IsNullOrEmpty(directoryName))
            {
                if (!Directory.Exists(directoryName))
                {
                    Directory.CreateDirectory(directoryName);
                }
            }
            using (var file = File.CreateText(fullFileName))
            {
                using (var writer = new JsonTextWriter(file))
                {
                    string json = JsonConvert.SerializeObject(jsonObject, Formatting.Indented, 
                        new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore } );
                    await writer.WriteRawAsync(json);
                }
            }
        }
    }
}

