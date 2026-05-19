using Microsoft.Extensions.DependencyInjection;
using PhoneBook.Core;
using System;

namespace PhoneBook.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IServiceProvider _serviceProvider;
        private ObservableObject _currentViewModel;

        public event Action CurrentViewModelChanged;

        public ObservableObject CurrentViewModel
        {
            get => _currentViewModel;
            private set
            {
                _currentViewModel = value;
                CurrentViewModelChanged?.Invoke();
            }
        }

        // Инжектируем IServiceProvider для динамического получения нужной ViewModel
        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void NavigateTo<TViewModel>() where TViewModel : ObservableObject
        {
            // Получаем запрошенную ViewModel из DI-контейнера и устанавливаем как текущую
            CurrentViewModel = _serviceProvider.GetRequiredService<TViewModel>();
        }
    }
}