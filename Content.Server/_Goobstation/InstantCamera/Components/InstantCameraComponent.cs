using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom.Prototype;

namespace Content.Server._Goobstation.InstantCamera.Components;

[RegisterComponent]
public sealed partial class InstantCameraComponent : Component
{
    /// <summary>
    ///  Amount of charges the Camera has.
    /// </summary>
    [DataField]
    public int? Charges;

    /// <summary>
    ///  Output of the Camera.
    /// </summary>
    [DataField("cameraOutput", customTypeSerializer: typeof(PrototypeIdSerializer<EntityPrototype>))]
    public string CameraOutput = "Photo";
}
