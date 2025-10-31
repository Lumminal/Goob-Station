using Robust.Shared.GameStates;

namespace Content.Goobstation.Shared.Nightmare.Components;

[RegisterComponent, NetworkedComponent]
public sealed partial class NightmareComponent : Component
{
    [DataField]
    public bool KnockdownOnJaunt = true;
};
