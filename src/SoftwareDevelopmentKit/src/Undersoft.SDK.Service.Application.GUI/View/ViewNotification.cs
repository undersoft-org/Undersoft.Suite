using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Service.Application.GUI.Models;
using Undersoft.SDK.Updating;

namespace Undersoft.SDK.Service.Application.GUI.View;

public class ViewNotification
{
    private IMessageService _messageService;

    public ViewNotification(IMessageService messageService)
    {
        _messageService = messageService;
    }

    public void Notify(Notification notification)
    {
        _messageService.ShowMessageBar(options => notification.Options.PatchTo(options));
    }

    public void NotifyOnTop(Notification notification)
    {
        _messageService.ShowMessageBar(options =>
        {
            options.Title = notification.Message;
            options.Intent = notification.Intent;
            options.Section = ViewSection.TOP;
            options.Timeout = notification.Timeout;
        });
    }

    public void NotifyInPanel(Notification notification)
    {
        _messageService.ShowMessageBar(options =>
        {
            options.Title = notification.Title;
            options.Intent = notification.Intent;
            options.Section = ViewSection.PANEL;
            options.Timeout = notification.Timeout;
            options.Body = notification.Message;
            options.Link = notification.Link;
            options.Timestamp = DateTime.Now;
            options.Timeout = notification.Timeout;
        });
    }

    public void NotifyInDialog(Notification notification)
    {
        _messageService.ShowMessageBar(options =>
        {
            options.Title = notification.Title;
            options.Intent = notification.Intent;
            options.Body = notification.Message;
            options.Link = notification.Link;
            options.Timeout = notification.Timeout;
            options.Timestamp = DateTime.Now;
            options.Section = ViewSection.DIALOG;
        });
    }
}
