using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CheckmateAcademy.Presentation.DependencyInjection;
using CheckmateAcademy.Presentation.Views.Auth;

namespace CheckmateAcademy.Presentation
{
    public partial class App : System.Windows.Application
    {
        private ServiceProvider _serviceProvider = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ShutdownMode = ShutdownMode.OnExplicitShutdown;

            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var services = new ServiceCollection();

            services.AddPresentationServices(configuration);
            services.AddTransient<AuthWindow>();
            services.AddSingleton<MainWindow>();

            _serviceProvider = services.BuildServiceProvider();

            var authWindow = _serviceProvider.GetRequiredService<AuthWindow>();

            if (authWindow.ShowDialog() != true)
            {
                Shutdown();
                return;
            }

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();

            MainWindow = mainWindow;
            ShutdownMode = ShutdownMode.OnMainWindowClose;

            mainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _serviceProvider?.Dispose();
            base.OnExit(e);
        }
    }
}
