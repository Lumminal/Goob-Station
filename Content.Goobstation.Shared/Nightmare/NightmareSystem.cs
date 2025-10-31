// SPDX-FileCopyrightText: 2025 GoobBot <uristmchands@proton.me>
// SPDX-FileCopyrightText: 2025 Lumminal <81829924+Lumminal@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Roudenn <romabond091@gmail.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Goobstation.Shared.LightDetection.Components;
using Content.Goobstation.Shared.Nightmare.Components;
using Content.Goobstation.Shared.PhaseShift;
using Content.Goobstation.Shared.Shadowling.Components.Abilities.Ascension;
using Content.Shared.Actions;
using Content.Shared.Stunnable;
using Content.Shared.Weapons.Reflect;

namespace Content.Goobstation.Shared.Nightmare;

/// <summary>
/// This handles nightmare logic
/// </summary>
public sealed class NightmareSystem : EntitySystem
{
    [Dependency] private readonly SharedActionsSystem _actionsSystem = default!;
    [Dependency] private readonly SharedStunSystem _stunSystem = default!;

    private EntityQuery<PhaseShiftedComponent> _phaseShiftedQuery;

    public override void Initialize()
    {
        base.Initialize();

        _phaseShiftedQuery = GetEntityQuery<PhaseShiftedComponent>();
    }

    public override void Update(float frameTime)
    {
        base.Update(frameTime);

        // Nightmares reflect shots while in the dark
        var nightmare = EntityQueryEnumerator<NightmareComponent, LightDetectionComponent, ReflectComponent>();
        while (nightmare.MoveNext(out var uid, out var night, out var lightDet, out var reflect))
        {
            if (lightDet.OnLight && _phaseShiftedQuery.HasComp(uid) && night.KnockdownOnJaunt)
            {
                RemComp<PhaseShiftedComponent>(uid);
                _stunSystem.TryKnockdown(uid, TimeSpan.FromSeconds(3), false);
                if (TryComp(uid, out ShadowlingPlaneShiftComponent? planeShift))
                    _actionsSystem.SetCooldown(planeShift.ActionEnt, TimeSpan.FromSeconds(3));
            }

            reflect.ReflectProb = lightDet.OnLight ? 0f : 1f;
        }
    }
}
