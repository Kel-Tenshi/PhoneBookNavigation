using Microsoft.Extensions.DependencyInjection;
using PhoneBook.Core;
using System;

namespace PhoneBook.Services
{
    public class NavigationService : ObservableObject, INavigationService
    {
        private readonly IServiceProvider _serviceProvider;
        private object? _currentViewModel;

        public object? CurrentViewModel
        {
            get => _currentViewModel;
            private set => Set(ref _currentViewModel, value);
        }

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void NavigateTo<TViewModel>(object? parameter = null) where TViewModel : class
        {
            // Получаем ViewModel из DI-контейнера
            var vm = _serviceProvider.GetRequiredService<TViewModel>();

            // Если ViewModel поддерживает прием параметров, передаем их
            if (vm is INavigationAware navigationAware)
            {
                navigationAware.OnNavigatedTo(parameter);
            }

            // Обновляем текущую ViewModel
            CurrentViewModel = vm;
        }
    }
}