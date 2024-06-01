﻿using Microsoft.FluentUI.AspNetCore.Components;
using System.Text.Json.Serialization;

namespace Undersoft.SDK.Service.Application.GUI.Models;

public class AppearanceState : ViewwModel
{
    public LocalizationDirection Dir { get; set; } = LocalizationDirection.LeftToRight;
    public StandardLuminance Luminance { get; set; } = StandardLuminance.DarkMode;

    public ElementReference Container { get; set; } = default!;

    public string? Color { get; set; } = "#00d7ff";

    public event Action? OnChange;

    public string Version { get; set; } = string.Empty;

    public bool IsDarkMode { get; set; }

    public bool IsDevice { get; set; }

    [JsonIgnore]
    public bool IsLoaded { get; set; }

    public int? Density { get; set; } = 0;

    public int? ControlCornerRadius { get; set; } = 0;

    public int? LayerCornerRadius { get; set; } = 0;

    public void SetDirection(LocalizationDirection dir)
    {
        Dir = dir;
        NotifyStateHasChanged();
    }

    public void SetLuminance(StandardLuminance luminance)
    {
        Luminance = luminance;
        NotifyStateHasChanged();
    }

    public void SetContainer(ElementReference container)
    {
        Container = container;
        NotifyStateHasChanged();
    }

    public void SetColor(string? color)
    {
        Color = color;
        NotifyStateHasChanged();
    }

    private void NotifyStateHasChanged()
    {
        OnChange?.Invoke();
    }
}
