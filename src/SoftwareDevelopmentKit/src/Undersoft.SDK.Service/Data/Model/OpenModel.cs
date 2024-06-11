using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Undersoft.SDK.Service.Data.Model;

using Identifier;
using Microsoft.OData.ModelBuilder;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Rubrics;
using Undersoft.SDK.Service.Data.Object;
using Undersoft.SDK.Service.Data.Object.Detail;
using Undersoft.SDK.Service.Data.Object.Group;
using Undersoft.SDK.Service.Data.Object.Setting;

[DataContract]
[StructLayout(LayoutKind.Sequential)]
public class OpenModel<TViewModel, TDetail, TSetting, TGroup> : DataObject, IViewModel
    where TViewModel : IDataObject
    where TDetail : class, IDetail, new()
    where TSetting : class, ISetting, new()
    where TGroup : class, IGroup
{
    private static IRubrics detailRubrics;
    private static IRubrics settingRubrics;

    public OpenModel() : base()
    {
    }

    [AutoExpand]
    [DataMember(Order = 12)]
    public virtual IdentifierSet<TViewModel> Identifiers { get; set; }

    [Details]
    [AutoExpand]
    [DataMember(Order = 13)]
    public virtual Listing<TDetail> Details { get; set; }

    [Settings]
    [AutoExpand]
    [DataMember(Order = 14)]
    public virtual Listing<TSetting> Settings { get; set; }

    [AutoExpand]
    [DataMember(Order = 15)]
    public virtual Listing<TGroup> Groups { get; set; }


    private IEnumerable<T> GetOpenProperties<T>(IRubrics openRubrics)
        where T : class, IDetail, new()
    {
        foreach (var r in openRubrics)
        {
            var detailMember = ((IInnerProxy)this)[r.RubricId];
            if (detailMember != null)
            {
                var objectDetail = new T() { Name = r.RubricName };
                objectDetail.SetDocument(detailMember);
                yield return objectDetail;
            }
        }
        ;
    }

    private void SetOpenProperties<T>(IEnumerable<T> openObjects) where T : class, IDetail, new()
    {
        openObjects.ForEach((o) => ((IInnerProxy)this)[o.Name] = o.GetObject());
    }

    private void GetOpenRubrics()
    {
        if (detailRubrics == null)
        {
            detailRubrics = new MemberRubrics(
                ((IInnerProxy)this).Proxy.Rubrics.Where(
                    r =>
                        r.CustomAttributes
                            .Select(c => c.AttributeType)
                            .Contains(typeof(DetailAttribute))
                )
            );
            settingRubrics = new MemberRubrics(
                ((IInnerProxy)this).Proxy.Rubrics.Where(
                    r =>
                        r.CustomAttributes
                            .Select(c => c.AttributeType)
                            .Contains(typeof(SettingAttribute))
                )
            );
        }
    }
}
