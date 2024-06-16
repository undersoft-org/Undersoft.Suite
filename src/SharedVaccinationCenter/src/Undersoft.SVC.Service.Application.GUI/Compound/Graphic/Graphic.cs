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
            @"<svg width=""160"" height=""30"" version=""1.1"" viewBox=""0 0 70.907 14.551"" xmlns=""http://www.w3.org/2000/svg"">
                     <g transform=""matrix(2.4897,0,0,3.6725,-52.463,-153.78)"" style=""stroke-width:.99997"">                    
                      <text transform=""scale(1.0673 .93693)"" x=""20.417986"" y=""47.365906"" fill-rule=""evenodd"" clip-rule=""evenodd""style=""font-family:sans-serif;font-size:18.036px;letter-spacing:.35px;line-height:73.8%;stroke-width:1.0906;word-spacing:-.90124px"" xml:space=""preserve""><tspan x=""20.417986"" y=""47.365906"" rotate=""0 0 0 0 0 0 0 0 0 0 0 0 0 0 0"" style=""font-family:'Segoe UI';font-size:3.2666px;font-variant-caps:normal;font-variant-east-asian:normal;font-variant-ligatures:normal;font-variant-numeric:normal;letter-spacing:.35px;line-height:73.8%;stroke-width:1.0906;word-spacing:-.90124px"">Vaccination</tspan></text>
                      <text transform=""scale(1.0977 .91103)"" x=""35.133987"" y=""50.103233"" style=""fill:#a2a2a2;font-family:sans-serif;font-size:9.1027px;letter-spacing:.78193px;line-height:125%;stroke-width:.97461;word-spacing:0px"" xml:space=""preserve""><tspan x=""35.133987"" y=""50.103233"" style=""fill:#a2a2a2;font-family:'Segoe UI';font-size:2.0852px;font-variant-caps:normal;font-variant-east-asian:normal;font-variant-ligatures:normal;font-variant-numeric:normal;font-variation-settings:'wght' 300;letter-spacing:.78193px;stroke-width:.97461"">center</tspan></text>
                      <text transform=""scale(1.2145 .82337)"" x=""31.183947"" y=""47.461952"" style=""fill:none;font-family:Sans;font-size:3.133px;font-variation-settings:'wght' 300;letter-spacing:0px;line-height:125%;stroke-width:.087498px;word-spacing:0px"" xml:space=""preserve""><tspan x=""31.183947"" y=""47.461952"" style=""stroke-width:.087498px""/></text>
                     </g>
                    </svg>"
        )
    { }
}
