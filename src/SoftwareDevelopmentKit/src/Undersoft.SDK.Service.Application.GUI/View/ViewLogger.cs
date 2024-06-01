namespace Undersoft.SDK.Service.Application.GUI.View;

public delegate void OnLogHandler(string text);

public static class ViewLogger
{
    public static event OnLogHandler? OnLogHandler;

    public static void WriteLine(string text)
    {
        OnLogHandler?.Invoke(text);
    }
}
