﻿namespace BeautySalon.UI;

public partial class App
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
        UserAppTheme = AppTheme.Light;
    }
}