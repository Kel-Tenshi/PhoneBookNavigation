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

            // Создаем коллекцию сервисов
            var services = new ServiceCollection();

            // Регистрация сервисов
            // DialogService регистрируем как Singleton из-за отсутсвия состояния
            services.AddSingleton<IDialogService, DialogService>();

            // Регистрация ViewModel как Transient
            // При каждом запросе будет создаваться новый экземпляр. 
            services.AddTransient<ContactsListViewModel>();

            // Регистрация Главного окна как Singleton с явной передачей DataContext.
            services.AddSingleton<MainWindow>(provider =>
            {
                var window = new MainWindow();
                // Контейнер сам всё сделает, он умный
                window.DataContext = provider.GetRequiredService<ContactsListViewModel>();
                return window;
            });

            // Провайдер сервисов
            var serviceProvider = services.BuildServiceProvider();

            // Запуск
            var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}