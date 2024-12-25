namespace JitterGang.Services;

public interface IJitterService
{
    // Основные операции
    void Start();
    void Stop();

    // Настройки джиттера
    void UpdateStrength(int strength);
    void UpdatePullDownStrength(int strength);
    void SetDelay(int delayMs);
    void UpdateJitters();
    void SetToggleKey(int keyCode);
    //
    void HandleShakeTimerTick();

    // Управление процессом
    void SetSelectedProcess(string processName);

    // Контроллер
    void SetUseController(bool useController);

    // Свойства
    bool IsCircleJitterActive { get; set; }
    bool UseAdsOnly { get; set; }
    bool IsRunning { get; }

    // Очистка ресурсов
    void Dispose();
}