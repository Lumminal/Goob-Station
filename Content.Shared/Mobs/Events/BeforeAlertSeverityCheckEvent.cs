// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Coolsurf6 <coolsurf24@yahoo.com.au>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Alert;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared.Mobs.Events;

/// <summary>
///     Event for allowing the interrupting and change of the mob threshold severity alert
/// </summary>
[Serializable, NetSerializable]
public sealed class BeforeAlertSeverityCheckEvent(ProtoId<AlertPrototype> currentAlert, short severity) : EntityEventArgs
{
    public bool CancelUpdate = false;
    public ProtoId<AlertPrototype> CurrentAlert = currentAlert;
    public short Severity = severity;
}