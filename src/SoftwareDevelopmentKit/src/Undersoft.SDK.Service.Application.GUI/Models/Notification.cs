using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Service.Application.GUI.View;
using Undersoft.SDK.Service.Data.Event;
using Undersoft.SDK.Updating;

namespace Undersoft.SDK.Service.Application.GUI.Models;

public class Notification : Event
{
    public Notification() { }

    public Notification(string message, MessageIntent notificationIntent = MessageIntent.Info)
    {
        Message = message;
        Intent = notificationIntent;
    }

    public Notification(string message, MessageIntent notificationIntent, string section)
    {
        Message = message;
        Intent = notificationIntent;
        Section = section;
    }

    public Notification(Event eventMessage, MessageIntent notificationIntent, string section)
    {
        Message = eventMessage.Data.ToString();
        Title = eventMessage.EventType;
        Intent = notificationIntent;
        Section = section;
    }

    public string? Title { get; set; }

    public int Timeout { get; set; }

    public Icon? Icon { get; set; }

    public string? Message { get; set; }

    public MessageIntent Intent { get; set; }

    public string? Section { get; set; } = ViewSection.TOP;

    public ActionLink<Message>? Link { get; set; }

    public event Action<Message>? OnClose;

    public MessageOptions? Options =>
        this.PatchTo(
            new MessageOptions() { Timestamp = DateTime.Now, ClearAfterNavigation = true }
        );
}
