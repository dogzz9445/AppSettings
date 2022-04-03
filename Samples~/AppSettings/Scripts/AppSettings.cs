using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mini.AppSettings;
using Newtonsoft.Json;

public class AppSettings : BindableBase
{
    #region Property
    // Display
    [JsonProperty("brightness")]
    private int? _brightness;
    [JsonProperty("gamma")]
    private int? _gamma;

    // UI
    [JsonProperty("ratioUI")]
    private float? _ratioUI;

    [JsonIgnore]
    public int? Brightness { get => _brightness; set => SetProperty(ref _brightness, value); }
    [JsonIgnore]
    public int? Gamma { get => _gamma; set => SetProperty(ref _gamma, value); }

    [JsonIgnore]
    public float? RatioUI { get => _ratioUI; set => SetProperty(ref _ratioUI, value); }

    #endregion

    public AppSettings(
        int? brightness = null,
        int? gamma = null,
        float? ratioUI = null)
    {
        Brightness = brightness ?? 50;
        Gamma = gamma ?? 50;
        RatioUI = ratioUI ?? 1.0f;
    }

    public override bool Equals(object otherAppSettings)
    {
        var other = otherAppSettings as AppSettings;
        if (other == null)
        {
            return false;
        }

        if (other.Brightness == this.Brightness &&
            other.Gamma == this.Gamma &&
            other.RatioUI == this.RatioUI)
        {
            return true;
        }
        return false;
    }

    public AppSettings() : this(null, null, null)
    {
    }

    public AppSettings Copy() => new AppSettings(
                    brightness: this.Brightness,
                    gamma: this.Gamma,
                    ratioUI: this.RatioUI);

    public override int GetHashCode()
    {
        int hashCode = 497968968;
        hashCode = hashCode * -1521134295 + Brightness.GetHashCode();
        hashCode = hashCode * -1521134295 + Gamma.GetHashCode();
        hashCode = hashCode * -1521134295 + RatioUI.GetHashCode();
        return hashCode;
    }

    public override string ToString()
    {
        return Brightness?.ToString();
    }
}
