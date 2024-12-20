using System.Diagnostics;
using System.Text.Json;
using JitterGang.Models;

namespace JitterGang.Services;

public class SettingsService : ISettingsService
{
    private readonly string _settingsFilePath;

    public SettingsService()
    {
        string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string appFolder = Path.Combine(documentsFolder, "JitterGang");

        if (!Directory.Exists(appFolder))
        {
            Directory.CreateDirectory(appFolder);
        }

        _settingsFilePath = Path.Combine(appFolder, "settings.json");
        Debug.WriteLine($"Settings file path: {_settingsFilePath}"); // Добавим для проверки
    }

    public string GetSettingsFilePath() => _settingsFilePath;

    public async Task<JitterSettings> LoadSettingsAsync()
    {
        try
        {
            Debug.WriteLine($"Loading settings from: {_settingsFilePath}");

            if (!File.Exists(_settingsFilePath))
            {
                Debug.WriteLine("Settings file not found!");
                return new JitterSettings();
            }

            string json = await File.ReadAllTextAsync(_settingsFilePath);
            Debug.WriteLine($"Read JSON content: {json}");

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };

            var settings = JsonSerializer.Deserialize<JitterSettings>(json, options);

            // Проверяем загруженные значения
            if (settings != null)
            {
                Debug.WriteLine($"Successfully loaded settings: " +
                              $"Strength={settings.Strength}, " +
                              $"PullDown={settings.PullDownStrength}");

                // Добавим валидацию загруженных настроек
                if (settings.Strength == 0)
                {
                    settings.Strength = 1;
                }

                return settings;
            }
            else
            {
                Debug.WriteLine("Failed to deserialize settings!");
                return new JitterSettings();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading settings: {ex}");
            return new JitterSettings();
        }
    }


    public async Task SaveSettingsAsync(JitterSettings settings)
    {
        try
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(settings, options);
            Debug.WriteLine($"Saving settings: {json}");

            await File.WriteAllTextAsync(_settingsFilePath, json);
            Debug.WriteLine("Settings saved successfully");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error saving settings: {ex}");
            throw;
        }
    }

    public async Task ResetToDefaultAsync()
    {
        await SaveSettingsAsync(new JitterSettings());
    }
}