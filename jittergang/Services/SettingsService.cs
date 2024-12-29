using JitterGang.Models;
using System.Diagnostics;
using System.Text.Json;
using System.IO; 

namespace JitterGang.Services;

public class SettingsService : ISettingsService
{
    private readonly string _settingsFilePath;

    private static readonly JsonSerializerOptions _serializerOptions = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };

    public SettingsService()
    {
        string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string appFolder = Path.Combine(documentsFolder, "JitterGang");
        Directory.CreateDirectory(appFolder);
        _settingsFilePath = Path.Combine(appFolder, "settings.json");
    }

    public string GetSettingsFilePath() => _settingsFilePath;

    public async Task<JitterSettings> LoadSettingsAsync()
    {
        try
        {
            if (!File.Exists(_settingsFilePath))
            {
                return new JitterSettings();
            }

            string json = await File.ReadAllTextAsync(_settingsFilePath);
            var settings = JsonSerializer.Deserialize<JitterSettings>(json, _serializerOptions);
            return settings ?? new JitterSettings();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading settings: {ex}");
            return new JitterSettings();
        }
    }


    public async Task SaveSettingsAsync(JitterSettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);

        try
        {
            string json = JsonSerializer.Serialize(settings, _serializerOptions);
            await File.WriteAllTextAsync(_settingsFilePath, json);
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