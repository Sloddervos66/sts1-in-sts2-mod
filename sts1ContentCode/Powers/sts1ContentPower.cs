using BaseLib.Abstracts;
using BaseLib.Extensions;
using sts1Content.sts1ContentCode.Extensions;
using Godot;

namespace sts1Content.sts1ContentCode.Powers;

public abstract class sts1ContentPower : CustomPowerModel
{
    //Loads from sts1Content/images/powers/your_power.png
    public override string CustomPackedIconPath
    {
        get
        {
            var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PowerImagePath();
            return ResourceLoader.Exists(path) ? path : "power.png".PowerImagePath();
        }
    }

    public override string CustomBigIconPath
    {
        get
        {
            var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigPowerImagePath();
            return ResourceLoader.Exists(path) ? path : "power.png".BigPowerImagePath();
        }
    }
}