using JetBrains.Annotations;

namespace Content.Server.NPC.HTN;

/// <summary>
/// This handles...
/// </summary>
public sealed class GoobHTNSystem : EntitySystem
{
    [Dependency] private readonly HTNSystem _htn = default!;

    // muh lacking of convenients overloads
    [PublicAPI]
    public void SetHTNEnabled(EntityUid uid, bool state, float planCooldown = 0f, HTNComponent? component = null)
    {
        if (!Resolve(uid, ref component))
            return;

        Entity<HTNComponent> ent = (uid, component);
        _htn.SetHTNEnabled(ent, state, planCooldown);
    }
}
