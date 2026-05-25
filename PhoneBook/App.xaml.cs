using Microsoft.Extensions.DependencyInjection;
using PhoneBook.Services;
using PhoneBook.ViewModels;
using System.Windows;

namespace PhoneBook
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();

            // Сервис диалоговых окон
            services.AddSingleton<IDialogService, DialogService>();

            // Сервис навигации
            services.AddSingleton<INavigationService, NavigationService>();

            // MainWindowViewModel управляет главным окном и навигацией
            services.AddSingleton<MainWindowViewModel>();

            // MainWindow - главное окно-контейнер приложения
            services.AddSingleton<MainWindow>(provider =>
            {
                var window = new MainWindow();
                window.DataContext = provider.GetRequiredService<MainWindowViewModel>();
                return window;
            });


            services.AddTransient<ContactsListViewModel>();
            services.AddTransient<AboutViewModel>();
            services.AddTransient<ContactEditViewModel>();

            var serviceProvider = services.BuildServiceProvider();

            // Запуск главного окна оболочки
            var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}