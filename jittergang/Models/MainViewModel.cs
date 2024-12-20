using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JitterGang.Models;
using JitterGang.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace JitterGang.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly ISettingsService _settingsService;
    private readonly IJitterService _jitterService;
    private bool _isInitialized;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanStart))]
    private bool _isRunning;

    public bool CanStart => !IsRunning;

    [ObservableProperty]
    private ObservableCollection<string> _processes;

    [ObservableProperty]
    private JitterSettings _settings;

    public MainViewModel(ISettingsService settingsService, IJitterService jitterService)
    {
        _settingsService = settingsService;
        _jitterService = jitterService;

        _processes = new ObservableCollection<string>();
        _settings = new JitterSettings();
    }

    [RelayCommand]
    private async Task Start(CancellationToken token = default)
    {
        try
        {
            if (!IsRunning)
            {
                ValidateSettings();
                Debug.WriteLine($"Settings: Strength={Settings.Strength}, " +
                          $"PullDown={Settings.PullDownStrength}, " +
                          $"Process={Settings.SelectedProcess}");

                int keyCode = ConvertKeyNameToCode(Settings.ToggleKey);
                _jitterService.SetToggleKey(keyCode);
                _jitterService.UpdateStrength(Settings.Strength);
                _jitterService.UpdatePullDownStrength(Settings.PullDownStrength);
                _jitterService.SetSelectedProcess(Settings.SelectedProcess);
                _jitterService.UpdateJitters();
                _jitterService.IsCircleJitterActive = Settings.IsCircleJitterActive;
                _jitterService.UseAdsOnly = Settings.UseAdsOnly;

                _jitterService.Start();
                IsRunning = true;
                Debug.WriteLine("Jitter service started"); // Отладка
                await SaveSettingsAsync();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error starting jitter: {ex.Message}");
            throw;
        }
    }

    [RelayCommand]
    private async Task Stop(CancellationToken token = default)
    {
        if (IsRunning)
        {
            _jitterService.Stop();
            IsRunning = false;
            await SaveSettingsAsync();
        }
    }

    public async Task InitializeAsync()
    {
        if (_isInitialized) return;

        try
        {
            var loadedSettings = await _settingsService.LoadSettingsAsync();
            if (loadedSettings != null)
            {
                Settings = loadedSettings; // Заменяем на загруженные настройки
            }

            // Применяем настройки к сервису
            _jitterService.UpdateStrength(Settings.Strength);
            _jitterService.UpdatePullDownStrength(Settings.PullDownStrength);
            _jitterService.IsCircleJitterActive = Settings.IsCircleJitterActive;
            _jitterService.UseAdsOnly = Settings.UseAdsOnly;

            if (Settings.UseController)
            {
                _jitterService.SetUseController(true);
            }

            await RefreshProcessList();

            _isInitialized = true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error in InitializeAsync: {ex.Message}");
            throw;
        }
    }

    private static int ConvertKeyNameToCode(string keyName)
    {
        return keyName switch
        {
            "F1" => 0x70,
            "F2" => 0x71,
            "F3" => 0x72,
            "F4" => 0x73,
            "F5" => 0x74,
            "F6" => 0x75,
            "F7" => 0x76,
            "F8" => 0x77,
            "F9" => 0x78,
            "F10" => 0x79,
            "F11" => 0x7A,
            "F12" => 0x7B,
            "Shift" => 0x10,
            "Capslock" => 0x14,
            "X1" => 0x05,
            "X2" => 0x06,
            _ => 0x70 // F1 по умолчанию
        };
    }

    [RelayCommand]
    public async Task RefreshProcessList(CancellationToken token = default)
    {
        try
        {
            var processList = Process.GetProcesses()
                .Select(p => p.ProcessName)
                .Distinct()
                .OrderBy(name => name)
                .ToList();

            // Создаем новую коллекцию вместо очистки существующей
            var newProcesses = new ObservableCollection<string>(processList);
            Processes = newProcesses; // Это вызовет уведомление об изменении

            // Сохраняем текущий выбранный процесс
            if (!string.IsNullOrEmpty(Settings.SelectedProcess) &&
                Processes.Contains(Settings.SelectedProcess))
            {
                Settings.SelectedProcess = Settings.SelectedProcess;
            }
            else if (Processes.Any())
            {
                Settings.SelectedProcess = Processes.First();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error refreshing process list: {ex.Message}");
            throw;
        }
    }

    public async Task SaveSettingsAsync()
    {
        try
        {
            await _settingsService.SaveSettingsAsync(Settings);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error saving settings: {ex.Message}");
            throw;
        }
    }

    private void ValidateSettings()
    {
        if (Settings.Strength < 1)
            Settings.Strength = 1;

        if (Settings.Delay < 1)
            Settings.Delay = 1;

        if (Settings.Strength == 0 && Settings.PullDownStrength == 0)
            Settings.Strength = 1;

        if (string.IsNullOrWhiteSpace(Settings.SelectedProcess))
            throw new ArgumentException("Process not selected");
    }

    public async Task UpdateControllerState(bool useController)
    {
        try
        {
            _jitterService.SetUseController(useController);
            Settings.UseController = useController;
            await SaveSettingsAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating controller state: {ex.Message}");
            throw;
        }
    }

    public void Cleanup()
    {
        _jitterService.Stop();
        _jitterService.Dispose();
    }
}