using Undersoft.SDK.Service.Data.Object.Setting;

namespace Undersoft.SSC.Service.Contracts.Settings;

[Setting]
public class ProfileSetting : DataObject
{
    public ProfileSetting() { }

    public string? AvatarPath { get; set; }

    public string? BoardPath { get; set; }

    public string? LogoPath { get; set; }

    public string? ProfilePath { get; set; }
}
