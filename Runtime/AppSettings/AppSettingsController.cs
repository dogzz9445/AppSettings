using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini.Settings.Examples
{
    public class AppSettingsController : ConsumerMonoBehaviour<AppSettings>
    {
        protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
