﻿using RecibosNominaCGA.VIEWMODELS;

namespace RecibosNominaCGA;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureEssentials()
            // Initialize the .NET MAUI Community Toolkit by adding the below line of code
            // After initializing the .NET MAUI Community Toolkit, optionally add additional fonts
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        builder.Services.AddTransient<ReciboViewModel>();
        // Continue initializing your .NET MAUI App here
        return builder.Build();
    }
}
