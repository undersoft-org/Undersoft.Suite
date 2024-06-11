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


    private IEnumerable<TDetail> GetDetailProperties(IRubrics openRubrics)
    {
        foreach (var r in openRubrics)
        {
            var detailMember = ((IInnerProxy)this)[r.RubricId];
            if (detailMember != null)
            {
                IInnerProxy detail = (IInnerProxy)detailMember;

                var objectDetail = new TDetail() { Name = r.RubricName };
                objectDetail.SetGeneral(detailMember);
                yield return objectDetail;
            }
        }
        ;
    }

    private void SetDetailProperties(IEnumerable<TDetail> openObjects)
    {
        openObjects.ForEach((o) => ((IInnerProxy)this)[o.Name] = o.GetDetail());
    }

    private void GetDetailRubrics() => detailRubrics ??= new MemberRubrics(
                ((IInnerProxy)this).Proxy.Rubrics.Where(
                    r =>
                        r.CustomAttributes
                            .Select(c => c.AttributeType)
                            .Contains(typeof(DetailAttribute))
                )
            );
    private IEnumerable<TDetail> GetSettingProperties(IRubrics openRubrics)
    {
        foreach (var r in openRubrics)
        {
            var detailMember = ((IInnerProxy)this)[r.RubricId];
            if (detailMember != null)
            {
                IInnerProxy detail = (IInnerProxy)detailMember;

                var objectDetail = new TDetail() { Name = r.RubricName };
                objectDetail.SetGeneral(detailMember);
                yield return objectDetail;
            }
        }
        ;
    }

    private void SetSettingProperties(IEnumerable<TDetail> openObjects)
    {
        openObjects.ForEach((o) => ((IInnerProxy)this)[o.Name] = o.GetDetail());
    }

    private void GetSettingRubrics() => settingRubrics ??= new MemberRubrics(
                ((IInnerProxy)this).Proxy.Rubrics.Where(
                    r =>
                        r.CustomAttributes
                            .Select(c => c.AttributeType)
                            .Contains(typeof(SettingAttribute))
                )
            );
}
