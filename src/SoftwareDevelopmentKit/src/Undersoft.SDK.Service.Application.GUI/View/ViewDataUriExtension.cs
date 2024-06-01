namespace Undersoft.SDK.Service.Application.GUI.View;

public static class ViewDataUriExtension
{
    public static string ToDataUri(this byte[] bytes, string mimeType)
    {
        return $"data:{mimeType};base64,{Convert.ToBase64String(bytes)}";
    }
}
