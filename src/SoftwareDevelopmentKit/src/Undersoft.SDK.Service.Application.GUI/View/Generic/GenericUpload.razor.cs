using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components.Utilities;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic
{
    public partial class GenericUpload : ViewItem
    {
        FluentInputFile? FileByStream = default!;
        int progressPercent;
        string? progressTitle;

        public string? Image { get; set; }

        [Parameter]
        public string DisplayName { get; set; } = default!;

        [Parameter]
        public bool Disabled { get; set; }

        List<string> Files = new();

        protected override void OnInitialized()
        {
            var filename = Data.Model.Proxy[Rubric.RubricId];
            if (filename != null)
            {
                Files.Add(filename.ToString()!.Split(';')[2]);
                var data = GetImageData();
                if (data != null)
                    Image = GetDataUri(filename.ToString()!, data);
            }

            base.OnInitialized();
        }

        async Task OnFileUploadedAsync(FluentInputFileEventArgs file)
        {
            Files.Clear();

            progressPercent = file.ProgressPercent;
            progressTitle = file.ProgressTitle;

            var fileInfo = $"{file.ContentType};{DateTime.Now.ToFileTime()};{file.Name}";
            Model.Proxy[Rubric.RubricId] = fileInfo;
            Files.Add(file.Name);

            var bytes = await file.Stream!.GetAllBytesAsync();

            SetImageData(bytes);
            Image = GetDataUri(fileInfo, bytes);

            StateHasChanged();

            await file.Stream!.DisposeAsync();
        }

        void OnCompleted(IEnumerable<FluentInputFileEventArgs> files)
        {
            progressPercent = FileByStream!.ProgressPercent;
            progressTitle = FileByStream!.ProgressTitle;
        }

        private string? ImageStyle => GetImageStyle();

        private string? StyleValue => GetStyle();

        public string? GetStyle()
        {
            var builder = new StyleBuilder(Style)
                .AddStyle("width: 100%")
                .AddStyle("height:140px")
                .AddStyle("background-color: transparent");
            return builder.Build();
        }

        public string? GetImageStyle()
        {
            var builder = new StyleBuilder()
                .AddStyle("width: ")
                .AddStyle("height: 140px");
            if (Image != null)
            {
                builder.AddStyle(GetBackgroundImage())
                .AddStyle("background-position: center")
                .AddStyle("background-repeat: no-repeat")
                .AddStyle("background-size: cover");
            }
            return builder.Build();
        }

        public string GetBackgroundImage()
        {
            if (Image != null)
                return $"background-image: url({Image})";
            return "";
        }

        public byte[]? GetImageData()
        {
            if (Rubric.DataMember != null && Model.Proxy[Rubric.DataMember] != null)
                return (byte[])Model.Proxy[Rubric.DataMember];
            else if (
                Model.Proxy.Rubrics.ContainsKey(Rubric.RubricName + "Data")
                && Model.Proxy[Rubric.RubricName + "Data"] != null
            )
                return (byte[])Model.Proxy[Rubric.RubricName + "Data"];
            return null;
        }

        public void SetImageData(byte[] bytes)
        {
            if (Rubric.DataMember != null)
                Model.Proxy[Rubric.DataMember] = bytes;
            else if (
                Model.Proxy.Rubrics.ContainsKey(Rubric.RubricName + "Data")
            )
                Model.Proxy[Rubric.RubricName + "Data"] = bytes;
        }

        private string? GetDataUri(string fileInfo, byte[] imageData)
        {
            var image = fileInfo;
            if (image != null && imageData != null)
            {
                return ((byte[])imageData).ToDataUri(image.Split(";")[0]);
            }
            return null;
        }
    }
}
