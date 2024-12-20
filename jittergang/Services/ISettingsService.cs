using JitterGang.Models;

namespace JitterGang.Services;

public interface ISettingsService
{
    // Загрузить настройки
    Task<JitterSettings> LoadSettingsAsync();

    // Сохранить настройки
    Task SaveSettingsAsync(JitterSettings settings);

    // Получить путь к файлу настроек
    string GetSettingsFilePath();

    // Сбросить настройки к значениям по умолчанию
    Task ResetToDefaultAsync();
}