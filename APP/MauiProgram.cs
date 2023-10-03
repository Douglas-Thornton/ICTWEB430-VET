using APP.States;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using APP.Services;
using APP.Interfaces;
using APP.Services.Email;

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
            builder.Services.AddSingleton<IConfiguration>(builder.Configuration.AddUserSecrets<App>().Build());

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<IUserService, UserService>();
            builder.Services.AddSingleton<IPetService, PetService>();
            builder.Services.AddSingleton<IEmailService, EmailService>();
            builder.Services.AddSingleton<LoggedUserState>();
            builder.Services.AddSingleton<App>();

            return builder.Build();
        }
    }
}