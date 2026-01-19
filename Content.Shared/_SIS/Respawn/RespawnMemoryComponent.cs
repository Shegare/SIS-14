using Robust.Shared.GameStates;

namespace Content.Shared._SIS.Respawn;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class RespawnStatusComponent : Component
{
    [DataField]
    public TimeSpan TimeToUnlockRespawn = TimeSpan.FromSeconds(10); // TODO-SIS: Сделать 10 минут, а не секунд

    [DataField, AutoNetworkedField]
    public TimeSpan TimeOfDeath;
}
