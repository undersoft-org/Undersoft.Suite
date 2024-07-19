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
    public bool IsGeneralizable<TAttrib>(IInnerProxy contract)
    {
        return IsDetalizable<TAttrib>(contract);
    }

    public bool IsGeneralizable(IInnerProxy contract, Type sourceAttribType)
    {
        var proxy = contract.Proxy;
        return proxy.Rubrics
            .Where(
                rubric =>
                    ((IMemberRubric)rubric.RubricInfo).MemberInfo
                        .GetCustomAttributes(sourceAttribType, false)
                        .FirstOrDefault() != null).Any();
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

    public IEnumerable<IDetail> Generalize<TSourceAttrib, TTargetAttrib>(IInnerProxy contract)
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
    }

    private ISeries<IDetail> GetGeneralized<TAttrib>(IInnerProxy contract)
    {
        return GetGeneralized(contract, typeof(TAttrib));
    }

    private ISeries<IDetail> GetGeneralized(IInnerProxy contract, Type sourceAttribType)
    {
        IMemberRubric resultRubric = null;
        var proxy = contract.Proxy;
        foreach (var rubric in proxy.Rubrics)
        {
            if (
                ((IMemberRubric)rubric.RubricInfo).MemberInfo
                    .GetCustomAttributes(sourceAttribType, false)
                    .FirstOrDefault() != null
            )
            {
                resultRubric = rubric;
                break;
            }
        }

        if (resultRubric == null || proxy[resultRubric.RubricId] == null)
            return null;
        return (ISeries<IDetail>)proxy[resultRubric.RubricId];
    }
}
