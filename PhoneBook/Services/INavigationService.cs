using PhoneBook.Core;
using System;

namespace PhoneBook.Services
{
    public interface INavigationService
    {
        // Текущая активная ViewModel
        ObservableObject CurrentViewModel { get; }

        // Событие, оповещающее об изменении активной ViewModel
        event Action CurrentViewModelChanged;

        // Метод для перехода к другой ViewModel
        void NavigateTo<TViewModel>() where TViewModel : ObservableObject;
    }
}