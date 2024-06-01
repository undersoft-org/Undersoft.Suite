// ------------------------------------------------------------------------
// MIT License - Copyright (c) Microsoft Corporation. All rights reserved.
// ------------------------------------------------------------------------

using Microsoft.AspNetCore.Components.Routing;
using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Invoking;
using Undersoft.SDK.Series;

namespace FluentUI.Demo.Shared;

public abstract class ViewNav
{
    public string Title { get; set; } = string.Empty;
    public NavLinkMatch Match { get; set; } = NavLinkMatch.Prefix;
    public Icon Icon { get; set; } = new Icons.Regular.Size20.Document();
    private string? _target;
    public string? Target
    {
        get { return _target; }
        set
        {
            if (value != null)
            {
                _target = value;
                _type = Assemblies.FindType(value);
                if (_type == null)
                    _type = Assemblies.FindTypeByFullName(value);
            }
        }
    }
    private Type? _type;
    public Type? Type
    {
        get { return _type; }
        set
        {
            _type = value;
            Target = value?.FullName;
        }
    }
    public string? Method { get; set; }
    private Invoker? _invoker;
    public Invoker? Invoker =>
        _invoker ??= _type == null || Method == null ? null : new Invoker(_type, Method);
}

public class NavInvoker : ViewNav
{
    public NavInvoker() { }

    public NavInvoker(string? targetName, string? method)
    {
        Target = targetName;
        Method = method;
    }

    public NavInvoker(Type targetType, string method)
    {
        Type = targetType;
        Method = method;
    }
}

public class NavLink : NavInvoker
{
    public string Href { get; set; } = default!;

    public NavLink() { }

    public NavLink(
        string href,
        Icon icon,
        string title,
        NavLinkMatch match = NavLinkMatch.Prefix,
        string? targetName = null,
        string? method = null
    )
    {
        Href = href;
        Icon = icon;
        Title = title;
        Match = match;
    }

    public NavLink(
        string href,
        Icon icon,
        string title,
        Type targetType,
        string method,
        NavLinkMatch match = NavLinkMatch.Prefix
    ) : base(targetType, method)
    {
        Href = href;
        Icon = icon;
        Title = title;
        Match = match;
    }
}

public class NavGroup : NavInvoker
{
    public bool Expanded { get; init; }
    public string Gap { get; set; } = default!;
    public ISeriesItem<ViewNav> Children { get; set; } = default!;

    public NavGroup() { }

    public NavGroup(
        Icon icon,
        string title,
        bool expanded,
        string gap,
        ISeriesItem<ViewNav> children,
        string? targetName = null,
        string? method = null
    ) : base(targetName, method)
    {
        Icon = icon;
        Title = title;
        Expanded = expanded;
        Gap = gap;
        Children = children;
    }

    public NavGroup(
        Icon icon,
        string title,
        bool expanded,
        string gap,
        Type targetType,
        string method,
        ISeriesItem<ViewNav> children
    ) : base(targetType, method)
    {
        Icon = icon;
        Title = title;
        Expanded = expanded;
        Gap = gap;
        Children = children;
    }
}
