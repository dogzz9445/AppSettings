using System.ComponentModel;
using System.Net;
using System.IO;
using System;
using System.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;
using Mini.Utils;

#nullable enable
namespace Mini.AppSettings
{
    public class Provider<T> : GlobalStateNotifier<T>
        where T : class, INotifyPropertyChanged, new()
    {
        private static Provider<T>? _global = null;
        public static Provider<T> Global
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

        private T? _context;
        public T Context 
        { 
            get 
            {
                if (_context == null)
                {
                    Load();
                }
                if (_context == null)
                {
                    _context = new T();
                }
                return _context;
            }
            set 
            {
                SetObservableProperty(ref _context, value);
            }
        }

        private string _contextFilePath;
        private string _contextFileName;
        public string ContextFullFileName { get => Path.Combine(_contextFilePath, _contextFileName); }
        
        private bool _isSaveContext;
        public bool IsSaveContext { get => _isSaveContext; set => _isSaveContext = value; }

        public Provider(
            bool? isSaveContext = null,
            string? filepath = null, 
            string? filename = null)
        {
            _isSaveContext = isSaveContext ?? true;
            _contextFilePath = filepath ?? Application.streamingAssetsPath;
            _contextFileName = filename ?? "app_settings.json";

            PropertyChanged += (s, e) => Save();

            Load();
        }

        public void Load()
        {
            Context = MiniJsonHelper.ReadFileOrDefault<T>(ContextFullFileName);
        }
        
        public async void Save()
        {
            if (IsSaveContext)
            {
                await MiniJsonHelper.WriteFileAsync(ContextFullFileName, Context);
            }
        }
    }
}

