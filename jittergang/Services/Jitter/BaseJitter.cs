using JitterGang.Win32;

namespace JitterGang.Services.Jitter;

public abstract class BaseJitter
{
    public abstract void ApplyJitter(ref INPUT input);
}