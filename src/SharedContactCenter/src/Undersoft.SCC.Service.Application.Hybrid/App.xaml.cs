// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   application: Undersoft.SCC.Service.Application.Hybrid
// ********************************************************



namespace Undersoft.SCC.Service.Application.Hybrid
{
    public partial class App : Microsoft.Maui.Controls.Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }
    }
}