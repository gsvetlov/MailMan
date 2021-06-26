using System;
using System.Windows;

using MailMan.Data;
using MailMan.Models;
using MailMan.Services;
using MailMan.Services.EMailAddressValidator;
using MailMan.Services.EntityEditorService;
using MailMan.Services.MailSenderService;
using MailMan.Services.Repositories.Base;
using MailMan.ViewModels;
using MailMan.ViewModels.UserDialog;

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
                .AddSingleton<IRepository<Server>, DebugServerRepository>()
                .AddSingleton<IRepository<Sender>, DebugSenderRepository>()
                .AddSingleton<IRepository<Recipient>, DebugRecipientRepository>()
                .AddSingleton<IRepository<Message>, DebugMessageRepository>()
                .AddSingleton<IRepository<MailingList>, DebugMailingListRepository>()
                .AddSingleton<IMailSenderService, DebugMailService>()
                .AddSingleton<IEmailAddressValidator, EmailAddressValidator>()
                .AddSingleton<IEntityEditorService<Server>, ServerEditorService>()
                .AddTransient<MainWindowViewModel>()
                .AddTransient<NotifyUserDialogViewModel>()
                .AddTransient<EditServerDialogVM>();
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
