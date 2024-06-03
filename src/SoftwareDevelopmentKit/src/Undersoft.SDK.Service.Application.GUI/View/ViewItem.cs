using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.FluentUI.AspNetCore.Components;
using System.ComponentModel;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Series;
using Undersoft.SDK.Service.Application.GUI.Models;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Uniques;

namespace Undersoft.SDK.Service.Application.GUI.View
{
    public class ViewItem<TModel> : ViewItem where TModel : class, IOrigin, IInnerProxy
    {
        protected override void OnInitialized()
        {
            if (Data == null)
            {
                Data = new ViewData<TModel>(typeof(TModel).New<TModel>());
            }
            base.OnInitialized();
        }

        public override TModel Model => Content.Model;

        [Parameter]
        public virtual IViewData<TModel> Content
        {
            get => (IViewData<TModel>)base.Data;
            set => base.Data = value;
        }
    }


    public class ViewItem : ComponentBase, IOrigin, IViewItem
    {
        protected long? typeId;

        protected IOrigin origin = new Origin();

        protected override void OnInitialized()
        {
            if (Parent != null)
                Parent.Children.Put(this);
            base.OnInitialized();
        }

        protected override void BuildRenderTree(RenderTreeBuilder __builder)
        {
            __builder.AddContent(0, ChildContent);
        }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        public virtual long Id
        {
            get => origin.Id;
            set => origin.Id = value;
        }

        public virtual long TypeId
        {
            get => typeId ??= this.GetType().UniqueKey32();
            set => typeId = value;
        }

        public virtual string ViewId => Id.ToString();

        [Parameter]
        public virtual object? Value
        {
            get => (Rubric != null) ? Data.Model.Proxy[Rubric.RubricId] : Data.Model;
            set
            {
                if (Rubric != null)
                    Data.Model.Proxy[Rubric.RubricId] = value;
                else if (value != null)
                {
                    Data.Model = (IInnerProxy)value;
                }
            }
        }

        public virtual IInnerProxy Model => Data.Model;

        [Parameter]
        public virtual IViewData Data
        {
            get => _data;
            set
            {
                _data = value;
                if (_data.ViewItem == null)
                    _data.ViewItem = this;
                BindingId = _data.Model.TypeId.ToString();
            }
        }
        private IViewData _data = default!;

        [Parameter]
        public virtual IViewRubric Rubric
        {
            get => _rubric;
            set
            {
                _rubric = value;
                if (_rubric.ViewItem == null)
                    _rubric.ViewItem = this;
                _rubric.FieldIdentifier = new FieldIdentifier(this, value.RubricName);
            }
        }
        private IViewRubric _rubric = default!;

        public object? Reference { get; set; } = default!;

        [Parameter]
        public virtual IViewItem? Root { get; set; }

        [Parameter]
        public virtual IViewItem? Parent { get; set; }

        [Parameter]
        public virtual ISeries<IViewItem> Children { get; set; } = new Listing<IViewItem>();

        [Parameter]
        public string? BindingId { get; set; }

        [Parameter]
        public virtual Icon? Icon { get; set; }

        [Parameter]
        public virtual string? Class { get; set; } = "";

        [Parameter]
        public virtual string? Style { get; set; }

        [Parameter]
        public virtual string? Attributes { get; set; }

        [Parameter]
        public virtual FeatureFlags FeatureFlags { get; set; } = new();

        [Parameter]
        public virtual StateFlags StateFlags { get; set; } = new();

        [Parameter]
        public virtual EditMode EditMode { get; set; }

        [Parameter]
        public virtual EntryMode EntryMode { get; set; }

        public string CodeNo
        {
            get => origin.CodeNo;
            set => origin.CodeNo = value;
        }

        public DateTime Created
        {
            get => origin.Created;
            set => origin.Created = value;
        }

        public string Creator
        {
            get => origin.Creator;
            set => origin.Creator = value;
        }

        public DateTime Modified
        {
            get => origin.Modified;
            set => origin.Modified = value;
        }

        public string Modifier
        {
            get => origin.Modifier;
            set => origin.Modifier = value;
        }

        [Parameter]
        public virtual string? Label
        {
            get => origin.Label;
            set => origin.Label = value;
        }

        public virtual int Index
        {
            get => origin.Index;
            set => origin.Index = value;
        }

        public string TypeName
        {
            get => origin.TypeName;
            set => origin.TypeName = value;
        }

        public DateTime Time
        {
            get => origin.Time;
            set => origin.Time = value;
        }

        public event PropertyChangedEventHandler? PropertyChanged
        {
            add { origin.PropertyChanged += value; }
            remove { origin.PropertyChanged -= value; }
        }

        public long AutoId()
        {
            return origin.AutoId();
        }

        public void GetFlag(Uniques.DataFlags state)
        {
            origin.GetFlag(state);
        }

        public byte GetPriority()
        {
            return origin.GetPriority();
        }

        public virtual void RenderView()
        {
            StateHasChanged();
        }

        public void SetFlag(Uniques.DataFlags state, bool flag)
        {
            origin.SetFlag(state, flag);
        }

        public long SetId(long id)
        {
            return origin.SetId(id);
        }

        public long SetId(object id)
        {
            return origin.SetId(id);
        }

        TEntity IOrigin.Sign<TEntity>(TEntity entity)
        {
            return origin.Sign(entity);
        }

        TEntity IOrigin.Stamp<TEntity>(TEntity entity)
        {
            return origin.Stamp(entity);
        }
    }
}
