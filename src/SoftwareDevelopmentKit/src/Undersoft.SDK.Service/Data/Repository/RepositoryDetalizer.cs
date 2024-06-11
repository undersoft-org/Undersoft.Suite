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

public partial class Repository<TEntity> : IRepositoryDetalizer<TEntity>
    where TEntity : class, IOrigin, IInnerProxy
{
    public bool IsDetalizable<TAttrib>(IInnerProxy contract)
    {
        return contract.Proxy.Rubrics.Where(r => r.CustomAttributes.Select(c => c.AttributeType).Contains(typeof(TAttrib))).Any();
    }

    public bool IsGeneralizable<TAttrib>(IInnerProxy contract)
    {
        return contract.Proxy.Rubrics
             .Where(r => r.CustomAttributes.Select(c => c.AttributeType).Contains(typeof(TAttrib)))
             .Any();
    }

    public bool IsGeneralizable(IInnerProxy contract, Type sourceAttribType)
    {
        return contract.Proxy.Rubrics
             .Where(r => r.CustomAttributes.Select(c => c.AttributeType).Contains(sourceAttribType))
             .Any();
    }

    public IEnumerable<IDetail> Generalize(IInnerProxy contract)
    {
        var generalDetails = Generalize<DetailAttribute, GeneralizedDetailsAttribute>(contract);
        var generalSettings = Generalize<DetailAttribute, GeneralizedSettingsAttribute>(contract);

        IEnumerable<IDetail> result = generalDetails;
        if (result == null)
            return result = generalSettings;
        else
            return result.Concat(generalSettings);

    }

    public IEnumerable<IDetail> Generalize
        <TSourceAttrib, TTargetAttrib>(
        IInnerProxy contract
    )
    {
        var detailStore = GetGeneralized<TTargetAttrib>(contract);
        var detailType = detailStore.GetType().GetEnumerableElementType();

        foreach (var tuple in GetDetalized<TSourceAttrib>(contract))
        {
            var detailMember = tuple.Item2;
            if (detailMember == null)
                continue;

            IInnerProxy detail = detailMember;

            var objectDetail = detailType.New<ISetting>();
            objectDetail.Name = tuple.Item1;

            objectDetail.SetGeneral(detailMember);

            yield return detailStore.Put((IDetail)objectDetail).Value;
        }
        ;
    }

    public IEnumerable<IDetail> Generalize(
        IInnerProxy contract,
        Type sourceAttribType,
        Type targetAttribType
    )
    {
        var detailStore = GetGeneralized(contract, targetAttribType);
        var detailType = detailStore.GetType().GetEnumerableElementType();

        foreach (var tuple in GetDetalized(contract, sourceAttribType))
        {
            var detailMember = tuple.Item2;
            if (detailMember == null)
                continue;

            IInnerProxy detail = detailMember;

            var objectDetail = detailType.New<IDetail>();
            objectDetail.Name = tuple.Item1;

            objectDetail.SetGeneral(detailMember);

            yield return detailStore.Put((IDetail)objectDetail).Value;
        }
        ;
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

    public IEnumerable<object> Detalize(
        IInnerProxy contract,
        Type targetAttribType
    )
    {
        var details = GetGeneralized(contract, targetAttribType);
        if (details == null || !details.Any())
            return null;
        return details.ForEach((o) => contract[o.Name] = o.GetDetail());
    }

    private IEnumerable<Tuple<string, IInnerProxy>> GetDetalized<TAttrib>(
        IInnerProxy contract
    )
    {
        return GetDetalized(contract, typeof(TAttrib));
    }

    private IEnumerable<Tuple<string, IInnerProxy>> GetDetalized(
        IInnerProxy contract,
        Type sourceAttribType
    )
    {
        var proxy = contract.Proxy;
        return proxy.Rubrics
            .Where(r => r.CustomAttributes.Select(c => c.AttributeType).Contains(sourceAttribType))
            .ForEach(
                r => new Tuple<string, IInnerProxy>(r.RubricName, (IInnerProxy)proxy[r.RubricId])
            );
    }

    public ISeries<IDetail> GetGeneralized<TAttrib>(IInnerProxy contract)
    {
        return GetGeneralized(contract, typeof(TAttrib));
    }

    public ISeries<IDetail> GetGeneralized(
        IInnerProxy contract,
        Type sourceAttribType
    )
    {
        var proxy = contract.Proxy;
        var rubric = proxy.Rubrics
            .Where(r => r.CustomAttributes.Select(c => c.AttributeType).Contains(sourceAttribType))
            .FirstOrDefault();

        if (rubric == null || proxy[rubric.RubricId] == null)
            return null;
        return (ISeries<IDetail>)proxy[rubric.RubricId];
    }
}
