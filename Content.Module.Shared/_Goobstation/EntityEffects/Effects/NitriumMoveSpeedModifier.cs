using Content.Shared.EntityEffects;
using Content.Shared.EntityEffects.Effects.StatusEffects;
using Content.Shared.Movement.Components;
using Content.Shared.Movement.Systems;
using Content.Shared.StatusEffectNew;
using Robust.Shared.Prototypes;

namespace Content.Server.EntityEffects.Effects;

public sealed partial class NitriumMovementSpeedModifierSystem : EntityEffectSystem<MovementSpeedModifierComponent, NitriumMovementSpeedModifier>
{
    [Dependency] private readonly StatusEffectsSystem _status = default!;
    [Dependency] private readonly MovementModStatusSystem _movementModStatus = default!;

    protected override void Effect(Entity<MovementSpeedModifierComponent> entity, ref EntityEffectEvent<NitriumMovementSpeedModifier> args)
    {
        var proto = args.Effect.EffectProto;
        var sprintMod = args.Effect.SprintSpeedModifier;
        var walkMod = args.Effect.WalkSpeedModifier;

        var duration = (args.Effect.Time ?? TimeSpan.FromSeconds(6f)) * args.Scale;

        switch (args.Effect.Type)
        {
            case StatusEffectMetabolismType.Update:
                 _movementModStatus.TryUpdateMovementSpeedModDuration(
                    entity,
                    proto,
                    duration,
                    sprintMod,
                    walkMod);
                break;
            case StatusEffectMetabolismType.Add:
                _movementModStatus.TryAddMovementSpeedModDuration(
                        entity,
                        proto,
                        duration,
                        sprintMod,
                        walkMod);
                break;

            case StatusEffectMetabolismType.Remove:
                _status.TryRemoveTime(entity, proto, duration);
                break;
            case StatusEffectMetabolismType.Set:
                _status.TrySetStatusEffectDuration(entity, proto, duration);
                break;
        }
    }
}

public sealed partial class NitriumMovementSpeedModifier : BaseStatusEntityEffect<NitriumMovementSpeedModifier>
{
    [DataField]
    public float WalkSpeedModifier = 1f;

    [DataField]
    public float SprintSpeedModifier = 1f;

    [DataField]
    public EntProtoId EffectProto = MovementModStatusSystem.ReagentSpeed;

    public override string? EntityEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys) =>
    Time == null
        ? null
        : Loc.GetString("reagent-effect-guidebook-movespeed-modifier",
            ("chance", Probability),
            ("walkspeed", WalkSpeedModifier),
            ("sprintspeed", SprintSpeedModifier),
            ("time", Time.Value.TotalSeconds));
}
