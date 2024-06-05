using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Service.Data.Model.Attributes;
using Undersoft.SDK.Uniques;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic
{
    public partial class GenericValue : ViewItem, IIdentifiable, IViewItem
    {
        private IProxy _proxy = default!;
        private int _index;
        private int _size;
        private bool _isFile { get; set; }
        private bool _isImage { get; set; }
        private bool _isBit { get; set; }
        private bool _isPersona { get; set; }

        private bool _bitValue
        {
            get => (Value != null) ? (bool)Value : false;
            set => Value = value;
        }
        private string? _label { get; set; } = default!;

        private string? _textValue
        {
            get { return Value?.ToString(); }
            set { Value = value; }
        }

        private string? _dataUri => GetDataUri();

        protected override void OnInitialized()
        {
            if (Data != null)
                _proxy = Data.Model.Proxy;
            _index = Rubric.RubricId;
            _size = Rubric.RubricSize;
            _isFile = Rubric.IsFile;
            _isImage = Rubric.IsImage;
            if (Rubric.ImageMode == ViewImageMode.Persona)
                _isPersona = true;
            _isBit = Rubric.RubricType == typeof(bool);
            Id = Model.Id.UniqueKey(Rubric.Id);
            TypeId = Model.TypeId;
            if (Rubric.Width != null)
                Width = Rubric.Width;
            if (Rubric.Height != null)
                Height = Rubric.Height;

            base.OnInitialized();
        }

        public string? TextValue { get => _textValue; }

        [Parameter]
        public string Width { get; set; } = "179px";

        [Parameter]
        public string Height { get; set; } = "28px";

        [CascadingParameter]
        public override IViewData Data { get; set; } = default!;

        public override object? Value
        {
            get { return _proxy[_index]; }
            set
            {
                _proxy[_index] = value;
            }
        }

        private string? GetDataUri()
        {
            if (_isFile)
            {
                var image = _textValue;
                var imageData = _proxy[Rubric.DataMember];
                if (image != null && imageData != null)
                {
                    return ((byte[])imageData).ToDataUri(image.Split(";")[0]);
                }
            }
            return null;
        }
    }
}
