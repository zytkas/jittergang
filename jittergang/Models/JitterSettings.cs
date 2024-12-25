using CommunityToolkit.Mvvm.ComponentModel;

namespace JitterGang.Models;

public partial class JitterSettings : ObservableObject
{
    // private поля для MVVM
    private int _strength;
    private int _pullDownStrength;
    private int _delay;
    private string _selectedProcess = string.Empty;
    private string _toggleKey = string.Empty;
    private bool _isCircleJitterActive;
    private bool _useController;
    private bool _useAdsOnly;

    // Публичные свойства с уведомлением об изменениях
    public int Strength
    {
        get => _strength;
        set => SetProperty(ref _strength, value);
    }

    public int PullDownStrength
    {
        get => _pullDownStrength;
        set => SetProperty(ref _pullDownStrength, value);
    }

    public int Delay
    {
        get => _delay;
        set => SetProperty(ref _delay, value);
    }

    public string SelectedProcess
    {
        get => _selectedProcess;
        set => SetProperty(ref _selectedProcess, value);
    }

    public string ToggleKey
    {
        get => _toggleKey;
        set => SetProperty(ref _toggleKey, value);
    }

    public bool IsCircleJitterActive
    {
        get => _isCircleJitterActive;
        set => SetProperty(ref _isCircleJitterActive, value);
    }

    public bool UseController   
    {
        get => _useController;
        set => SetProperty(ref _useController, value);
    }

    public bool UseAdsOnly
    {
        get => _useAdsOnly;
        set => SetProperty(ref _useAdsOnly, value);
    }

    public JitterSettings()
    {
        // Значения по умолчанию
        Strength = 1;
        PullDownStrength = 0;
        Delay = 1;
        SelectedProcess = string.Empty;
        ToggleKey = "F1";
        IsCircleJitterActive = false;
        UseController = false;
        UseAdsOnly = false;
    }
}