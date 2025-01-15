using Content.Server._Goobstation.InstantCamera.Components;
using Content.Server.Spawners.EntitySystems;
using Content.Shared.Charges.Systems;
using Content.Shared.DoAfter;
using Content.Shared.Hands.EntitySystems;
using Content.Shared.Interaction;
using Content.Shared.Interaction.Events;
using Content.Shared.Popups;

namespace Content.Server._Goobstation.InstantCamera.Systems;

public sealed class InstantCameraSystem : EntitySystem
{
    private const int Maxcharges = 30;
    [Dependency] private readonly SharedPopupSystem _popup = default!;
    [Dependency] private readonly SharedHandsSystem _handsSystem = default!;
    [Dependency] private readonly SharedChargesSystem _charges = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<InstantCameraComponent, UseInHandEvent>(TakePhoto);
        SubscribeLocalEvent<InstantCameraComponent, DoAfterEvent>(DoAfterPhoto);
    }

    private void TakePhoto(EntityUid uid, InstantCameraComponent comp, UseInHandEvent args)
    {
        if (!args.Handled)
            return;

        // Fail
        if (comp.Charges > Maxcharges)
            return;

        var photo = EntityManager.SpawnEntity(comp.CameraOutput, Transform(uid).Coordinates);
        _handsSystem.PickupOrDrop(args.User, photo, checkActionBlocker: false);

    }

    private void DoAfterPhoto(EntityUid uid, InstantCameraComponent comp, DoAfterEvent args)
    {

    }

}
