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
using Undersoft.SDK.Utilities;

public partial class Repository<TEntity> : IRepositoryDocument<TEntity>
    where TEntity : class, IOrigin, IInnerProxy
{
    public bool IsDocumentDeserializable<TAttrib>(IInnerProxy contract)
    {
        return IsDocumentSerializable(contract, typeof(TAttrib));
    }

    public IEnumerable<object> DeserializeDocuments(IInnerProxy contract)
    {
        var details = DeserializeDocuments<DetailAttribute>(contract);
        var settings = DeserializeDocuments<SettingAttribute>(contract);
        IEnumerable<object> resultt = details;
        if (resultt == null)
            return resultt = settings;
        else
            return resultt.Concat(settings);
    }

    public IEnumerable<object> DeserializeDocuments<TAttrib>(IInnerProxy contract)
    {
        var details = GetStructureDocuments<TAttrib>(contract);
        if (details == null || !details.Any())
            return null;
        return details.ForEach((o) => contract[o.Name] = o.GetStructure());
    }

    public IEnumerable<object> DeserializeDocuments(IInnerProxy contract, Type targetAttribType)
    {
        var details = GetStructureDocuments(contract, targetAttribType);
        if (details == null || !details.Any())
            return null;
        return details.ForEach((o) => contract[o.Name] = o.GetStructure());
    }

    public bool IsDocumentSerializable<TAttrib>(IInnerProxy contract)
    {
        return IsDocumentDeserializable<TAttrib>(contract);
    }

    public bool IsDocumentSerializable(IInnerProxy contract, Type sourceAttribType)
    {
        var proxy = contract.Proxy;
        return proxy.Rubrics
            .Where(
                rubric =>
                    ((IMemberRubric)rubric.RubricInfo).MemberInfo
                        .GetCustomAttributes(sourceAttribType, false)
                        .FirstOrDefault() != null).Any();
    }

    public IEnumerable<IDetail> SerializeDocuments(IInnerProxy contract)
    {
        var generalDetails = SerializeDocuments<DetailAttribute, GeneralizedDetailsAttribute>(contract);
        var generalSettings = SerializeDocuments<DetailAttribute, GeneralizedSettingsAttribute>(contract);

        IEnumerable<IDetail> result = generalDetails;
        if (result == null)
            return result = generalSettings;
        else
            return result.Concat(generalSettings);
    }

    public IEnumerable<IDetail> SerializeDocuments<TSourceAttrib, TTargetAttrib>(IInnerProxy contract)
    {
        var detailStore = GetStructureDocuments<TTargetAttrib>(contract);
        var detailType = detailStore.GetType().GetEnumerableElementType();

        foreach (var tuple in GetDocumentStructures<TSourceAttrib>(contract))
        {
            var detailMember = tuple.Item2;
            if (detailMember == null)
                continue;

            IInnerProxy detail = detailMember;

            var objectDetail = detailType.New<ISetting>();
            objectDetail.Name = tuple.Item1;

            objectDetail.SetDocument(detailMember);

            yield return detailStore.Put((IDetail)objectDetail).Value;
        }
    }

    public IEnumerable<IDetail> SerializeDocuments(
        IInnerProxy contract,
        Type sourceAttribType,
        Type targetAttribType
    )
    {
        var detailStore = GetStructureDocuments(contract, targetAttribType);
        var detailType = detailStore.GetType().GetEnumerableElementType();

        foreach (var tuple in GetDocumentStructures(contract, sourceAttribType))
        {
            var detailMember = tuple.Item2;
            if (detailMember == null)
                continue;

            IInnerProxy detail = detailMember;

            var objectDetail = detailType.New<IDetail>();
            objectDetail.Name = tuple.Item1;

            objectDetail.SetDocument(detailMember);

            yield return detailStore.Put((IDetail)objectDetail).Value;
        }
    }

    private ISeries<IDetail> GetStructureDocuments<TAttrib>(IInnerProxy contract)
    {
        return GetStructureDocuments(contract, typeof(TAttrib));
    }

    private ISeries<IDetail> GetStructureDocuments(IInnerProxy contract, Type sourceAttribType)
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

    private IEnumerable<Tuple<string, IInnerProxy>> GetDocumentStructures<TAttrib>(IInnerProxy contract)
    {
        return GetDocumentStructures(contract, typeof(TAttrib));
    }

    private IEnumerable<Tuple<string, IInnerProxy>> GetDocumentStructures(
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
