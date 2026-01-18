// SPDX-FileCopyrightText: 2023-2025 Space Wizards Federation
// SPDX-FileCopyrightText: 2026 Skill Issue Station contributors
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Actions;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared.Ghost;

/// <summary>
/// Represents an observer ghost.
/// Handles limiting interactions, using ghost abilities, ghost visibility, and ghost warping.
/// </summary>
[RegisterComponent, NetworkedComponent, Access(typeof(SharedGhostSystem))]
[AutoGenerateComponentState(true), AutoGenerateComponentPause]
public sealed partial class GhostComponent : Component
{
    // Actions
    [DataField]
    public EntProtoId ToggleLightingAction = "ActionToggleLighting";

    [DataField, AutoNetworkedField]
    public EntityUid? ToggleLightingActionEntity;

    [DataField]
    public EntProtoId ToggleFoVAction = "ActionToggleFov";

    [DataField, AutoNetworkedField]
    public EntityUid? ToggleFoVActionEntity;

    [DataField]
    public EntProtoId ToggleGhostsAction = "ActionToggleGhosts";

    [DataField, AutoNetworkedField]
    public EntityUid? ToggleGhostsActionEntity;

    [DataField]
    public EntProtoId ToggleGhostHearingAction = "ActionToggleGhostHearing";

    [DataField]
    public EntityUid? ToggleGhostHearingActionEntity;

    [DataField]
    public EntProtoId BooAction = "ActionGhostBoo";

    [DataField, AutoNetworkedField]
    public EntityUid? BooActionEntity;

    // SIS-Ghost_Respawn Start
    [DataField]
    public EntProtoId RespawnAction = "RespawnAction";

    [DataField, AutoNetworkedField]
    public EntityUid? RespawnActionEntity;
    // SIS-Ghost_Respawn Start

    // End actions

    /// <summary>
    /// Time at which the player died and created this ghost.
    /// Used to determine votekick eligibility.
    /// </summary>
    /// <remarks>
    /// May not reflect actual time of death if this entity has been paused,
    /// but will give an accurate length of time <i>since</i> death.
    /// </remarks>
    [DataField, AutoPausedField]
    public TimeSpan TimeOfDeath = TimeSpan.Zero;

    /// <summary>
    /// Range of the Boo action.
    /// </summary>
    [DataField]
    public float BooRadius = 3;

    /// <summary>
    /// Maximum number of entities that can affected by the Boo action.
    /// </summary>
    [DataField]
    public int BooMaxTargets = 3;

    /// <summary>
    /// Is this ghost allowed to interact with entities?
    /// </summary>
    /// <remarks>
    /// Used to allow admins ghosts to interact with the world.
    /// Changed by <see cref="SharedGhostSystem.SetCanGhostInteract"/>.
    /// </remarks>
    [DataField("canInteract"), AutoNetworkedField]
    public bool CanGhostInteract;

    /// <summary>
    /// Is this ghost player allowed to return to their original body?
    /// </summary>
    /// <remarks>
    /// Changed by <see cref="SharedGhostSystem.SetCanReturnToBody"/>.
    /// </remarks>
    [DataField, AutoNetworkedField]
    public bool CanReturnToBody;

    /// <summary>
    /// Ghost color
    /// </summary>
    /// <remarks>Used to allow admins to change ghost colors. Should be removed if the capability to edit existing sprite colors is ever added back.</remarks>
    [DataField, AutoNetworkedField]
    public Color Color = Color.White;
}

public sealed partial class ToggleFoVActionEvent : InstantActionEvent { }

public sealed partial class ToggleGhostsActionEvent : InstantActionEvent { }

public sealed partial class ToggleLightingActionEvent : InstantActionEvent { }

public sealed partial class ToggleGhostHearingActionEvent : InstantActionEvent { }

public sealed partial class ToggleGhostVisibilityToAllEvent : InstantActionEvent { }

public sealed partial class BooActionEvent : InstantActionEvent { }

// SIS-Ghost_Respawn Start
public sealed partial class RespawnActionEvent : InstantActionEvent;

[Serializable, NetSerializable]
public enum RespawnUiKey : byte
{
    Key,
}
// SIS-Ghost_Respawn End
