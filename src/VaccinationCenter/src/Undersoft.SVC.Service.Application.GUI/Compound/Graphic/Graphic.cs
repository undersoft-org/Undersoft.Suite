using Microsoft.FluentUI.AspNetCore.Components;

// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC.Service.Application.GUI
// ********************************************************

namespace Undersoft.SVC.Service.Application.GUI.Compound.Graphic;

/// <summary>
/// The logo SCC.
/// </summary>
public class LogoSVC : Icon
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LogoSVC"/> class.
    /// </summary>
    public LogoSVC()
        : base(
            "Undersoft",
            IconVariant.Regular,
            IconSize.Custom,
            @"<svg width=""120"" height=""30"" version=""1.1"" viewBox=""0 0 36.776 10.053"" xmlns=""http://www.w3.org/2000/svg"">
             <g transform=""matrix(2.4897,0,0,3.6725,-52.463,-153.78)"" style=""stroke-width:.99997"">
              <text transform=""scale(1.1065 .90379)"" x=""19.260561"" y=""48.130234"" ill-rule=""evenodd"" clip-rule=""evenodd"" style=""font-family:sans-serif;font-size:11.771px;letter-spacing:.22843px;line-height:73.8%;stroke-width:.71179;word-spacing:-.5882px"" xml:space=""preserve""><tspan x=""19.260561"" y=""48.130234"" rotate=""0 0 0 0 0 0 0 0 0 0 0 0"" style=""font-family:'Segoe UI';font-size:2.132px;font-variant-caps:normal;font-variant-east-asian:normal;font-variant-ligatures:normal;font-variant-numeric:normal;letter-spacing:.22843px;line-height:73.8%;stroke-width:.71179;word-spacing:-.5882px"">Vaccination</tspan></text>
              <text transform=""scale(1.1379 .87878)"" x=""24.910313"" y=""50.407543"" style=""fill:#a2a2a2;font-family:sans-serif;font-size:5.9411px;letter-spacing:.51035px;line-height:125%;stroke-width:.6361;word-spacing:0px"" xml:space=""preserve""><tspan x=""24.910313"" y=""50.407543"" style=""fill:#a2a2a2;font-family:'Segoe UI';font-size:1.361px;font-variant-caps:normal;font-variant-east-asian:normal;font-variant-ligatures:normal;font-variant-numeric:normal;font-variation-settings:'wght' 300;letter-spacing:.51035px;stroke-width:.6361"">center</tspan></text>
              <text transform=""scale(1.2145 .82337)"" x=""31.183947"" y=""47.461952"" style=""fill:none;font-family:Sans;font-size:3.133px;font-variation-settings:'wght' 300;letter-spacing:0px;line-height:125%;stroke-width:.087498px;stroke:#000000;word-spacing:0px"" xml:space=""preserve""><tspan x=""31.183947"" y=""47.461952"" style=""stroke-width:.087498px""/></text>
             </g>
            </svg>"
        )
    { }
}
