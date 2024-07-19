// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License.
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
// *************************************************

namespace Undersoft.SDK.Service.Data.Repository;

using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Object.Detail;
using Undersoft.SDK.Service.Data.Object.Setting;

public partial class Repository<TEntity>
    where TEntity : class, IOrigin, IInnerProxy
{
    public bool IsDetalizable<TAttrib>(IInnerProxy contract)
    {
        return IsGeneralizable(contract, typeof(TAttrib));
    }

    public IEnumerable<object> Detalize(IInnerProxy contract)
    {
        var details = Detalize<DetailAttribute>(contract);
        var settings = Detalize<SettingAttribute>(contract);
        IEnumerable<object> resultt = details;
        if (resultt == null)
            return resultt = settings;
        else
            return resultt.Concat(settings);
    }

    public IEnumerable<object> Detalize<TAttrib>(IInnerProxy contract)
    {
        var details = GetGeneralized<TAttrib>(contract);
        if (details == null || !details.Any())
            return null;
        return details.ForEach((o) => contract[o.Name] = o.GetDetail());
    }

    public IEnumerable<object> Detalize(IInnerProxy contract, Type targetAttribType)
    {
        var details = GetGeneralized(contract, targetAttribType);
        if (details == null || !details.Any())
            return null;
        return details.ForEach((o) => contract[o.Name] = o.GetDetail());
    }

    private IEnumerable<Tuple<string, IInnerProxy>> GetDetalized<TAttrib>(IInnerProxy contract)
    {
        return GetDetalized(contract, typeof(TAttrib));
    }

    private IEnumerable<Tuple<string, IInnerProxy>> GetDetalized(
        IInnerProxy contract,
        Type targetAttributeType
    )
    {
        var proxy = contract.Proxy;
        return proxy.Rubrics
            .ForOnly(
                rubric =>
                    ((IMemberRubric)rubric.RubricInfo).MemberInfo
                        .GetCustomAttributes(targetAttributeType, false)
                        .FirstOrDefault() != null,
                r => new Tuple<string, IInnerProxy>(r.RubricName, (IInnerProxy)proxy[r.RubricId])
            )
            .Commit();
    }
}
