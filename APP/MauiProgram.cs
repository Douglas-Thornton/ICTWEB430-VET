using APP.States;
using Microsoft.Extensions.Logging;
using APP.Services;
using APP.Interfaces;
using APP.Data;

namespace APP
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
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<IUserService, UserService>();
            builder.Services.AddSingleton<IPetService, PetService>();
            builder.Services.AddSingleton<LoggedUserState>();
            builder.Services.AddSingleton<App>();

            return builder.Build();
        }
    }
}