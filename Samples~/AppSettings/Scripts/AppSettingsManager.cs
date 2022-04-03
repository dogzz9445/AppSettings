using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Mini.AppSettings;

public class AppSettingsManager : ConsumerMonoBehaviour<AppSettings>
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        if (Provider<AppSettings>.Global == null)
        {
            Debug.Log("Global is null");
        }
        Debug.Log(Provider<AppSettings>.Global.AppSettings.Brightness);
        Screen.brightness = ((float)(Provider<AppSettings>.Global.AppSettings.Brightness ?? 100)) / 100.0f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        Screen.brightness = ((float)(Provider<AppSettings>.Global.AppSettings.Brightness ?? 100)) / 100.0f;
    }

}
