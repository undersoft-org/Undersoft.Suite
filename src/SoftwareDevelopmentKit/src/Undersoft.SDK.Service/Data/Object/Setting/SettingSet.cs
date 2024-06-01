using Undersoft.SDK.Service.Data.Object.Detail;

namespace Undersoft.SDK.Service.Data.Object.Setting;

public class SettingSet<TDto> : DetailSet<TDto> where TDto : class, ISetting
{
    public SettingSet() { }

    public SettingSet(IEnumerable<TDto> list) { list.ForEach(item => base.Add(item)); }
}
