using System;
using System.Windows;

using MailMan.Data;
using MailMan.Models;
using MailMan.Services;
using MailMan.Services.EMailAddressValidator;
using MailMan.Services.EntityEditorService;
using MailMan.Services.MailSenderService;
using MailMan.Services.Repositories;
using MailMan.Services.Repositories.Base;
using MailMan.Services.Repositories.Db;
using MailMan.ViewModels;
using MailMan.ViewModels.UserDialog;

using Microsoft.EntityFrameworkCore;
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
                .AddDbContext<MailManDB>(opt => opt.UseSqlServer(host.Configuration.GetConnectionString("dbConnection")))
                .AddScoped<IRepositoryAsync<Server>, ServerRepository>()
                .AddScoped<IRepositoryAsync<Sender>, SenderRepository>()
                .AddScoped<IRepositoryAsync<Recipient>, RecipientRepository>()
                .AddScoped<IRepositoryAsync<Message>, MessageRepository>()
                .AddScoped<IRepositoryAsync<MailingList>, MailingListRepository>()
                .AddScoped<IMailSenderService, DebugMailService>()
                .AddScoped<IEmailAddressValidator, EmailAddressValidator>()
                .AddScoped<IEntityEditorService<Server>, ServerEditorService>()
                .AddScoped<MainWindowViewModel>()
                .AddScoped<NotifyUserDialogViewModel>()
                .AddScoped<EditServerDialogVM>();
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
