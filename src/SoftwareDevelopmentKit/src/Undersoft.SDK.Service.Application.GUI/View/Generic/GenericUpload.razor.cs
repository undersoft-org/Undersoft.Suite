using Microsoft.FluentUI.AspNetCore.Components;

namespace Undersoft.SDK.Service.Application.GUI.View.Generic
{
    public partial class GenericUpload : ViewItem
    {
        FluentInputFile? FileByStream = default!;
        int progressPercent;
        string? progressTitle;

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

            if (Rubric.DataMember != null)
                Model.Proxy[Rubric.DataMember] = await file.Stream!.GetAllBytesAsync();
            else if (Model.Proxy.Rubrics.ContainsKey(Rubric.RubricName + "Data"))
                Model.Proxy[Rubric.RubricName + "Data"] = await file.Stream!.GetAllBytesAsync();

            await file.Stream!.DisposeAsync();
        }

        void OnCompleted(IEnumerable<FluentInputFileEventArgs> files)
        {
            progressPercent = FileByStream!.ProgressPercent;
            progressTitle = FileByStream!.ProgressTitle;
        }
    }
}
