using Robust.Shared.Timing;

namespace Content.Shared._SIS.Respawn;

public abstract class SharedRespawnSystem : EntitySystem
{
    [Dependency] private readonly IGameTiming _timing = default!;

    public TimeSpan GetRespawnCooldown(RespawnStatusComponent comp)
    {
        var unlockTime = comp.TimeToUnlockRespawn + comp.TimeOfDeath;
        var cooldown = unlockTime - _timing.CurTime;
        return cooldown;
    }
}
