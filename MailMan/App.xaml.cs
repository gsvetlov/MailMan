using System;
using System.Windows;

using MailMan.Data;
using MailMan.Services.MailSenderService;
using MailMan.Services.Repositories;
using MailMan.ViewModels;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MailMan
{

    public partial class App : Application
    {

        private static IHost _AppHost;
        public static IHost AppHost => _AppHost ??= CreateHostBuilder(Environment.GetCommandLineArgs()).Build();
        public static IServiceProvider Services => AppHost.Services;

        public static IHostBuilder CreateHostBuilder(string[] args) => Host
            .CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(opt => opt.AddUserSecrets<App>())
            .ConfigureLogging(opt => opt.AddDebug())
            .ConfigureServices(ConfigureServices);
        private static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services
                .AddSingleton<IServerRepository, DebugServerRepository>()
                .AddSingleton<ISenderRepository, DebugSenderRepository>()
                .AddSingleton<IRecipientRepository, DebugRecipientRepository>()
                .AddSingleton<IMessageRepository, DebugMessageRepository>()
                .AddSingleton<IMailingListRepository, DebugMailingListRepository>()
                .AddSingleton<IMailSenderService, DebugMailService>()
                .AddTransient<MainWindowViewModel>()
                .AddTransient<NotifyUserDialogViewModel>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            AppHost.Start();
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            AppHost.Dispose();
        }
    }
}
