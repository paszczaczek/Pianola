﻿using Microsoft.Extensions.Logging;

namespace Pianola.MAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("feta.ttf");
                });
            
            // https://youtu.be/xx1mve2AQr4?t=617
            // https://learn.microsoft.com/en-us/dotnet/architecture/maui/dependency-injection
            builder.Services
                .AddSingleton<MainPage>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}