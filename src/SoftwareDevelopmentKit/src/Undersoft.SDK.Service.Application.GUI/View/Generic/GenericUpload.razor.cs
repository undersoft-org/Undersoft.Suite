using Microsoft.FluentUI.AspNetCore.Components;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic
{
    public partial class GenericUpload : ViewItem
    {
        FluentInputFile? FileByStream = default!;
        int progressPercent;
        string? progressTitle;

        public string? Image { get; set; }

        private string? _backgroudImage => Image != null ? $"background-image:{Image}" : null;

        [Parameter]
        public string DisplayName { get; set; } = default!;

        List<string> Files = new();

        protected override void OnInitialized()
        {
            var filename = Data.Model.Proxy[Rubric.RubricId];
            if (filename != null)
                Files.Add(filename.ToString()!.Split(';')[2]);

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

            if (Rubric.DataMember != null)
                Model.Proxy[Rubric.DataMember] = bytes;
            else if (Model.Proxy.Rubrics.ContainsKey(Rubric.RubricName + "Data"))
                Model.Proxy[Rubric.RubricName + "Data"] = bytes;

            Image = GetDataUri(fileInfo, bytes);

            await file.Stream!.DisposeAsync();
        }

        void OnCompleted(IEnumerable<FluentInputFileEventArgs> files)
        {
            progressPercent = FileByStream!.ProgressPercent;
            progressTitle = FileByStream!.ProgressTitle;
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
