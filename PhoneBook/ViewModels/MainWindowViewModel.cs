using PhoneBook.Core;
using PhoneBook.Services;
using System.Windows.Input;

namespace PhoneBook.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        private readonly INavigationService _navigation;

        public INavigationService NavigationService => _navigation;

        public ICommand ShowContactsCommand { get; }
        public ICommand ShowAboutCommand { get; }

        public MainWindowViewModel(INavigationService navigation)
        {
            _navigation = navigation;

            ShowContactsCommand = new RelayCommand(
                () => _navigation.NavigateTo<ContactsListViewModel>());

            ShowAboutCommand = new RelayCommand(
                () => _navigation.NavigateTo<AboutViewModel>());

            // Стартовый экран по умолчанию
            _navigation.NavigateTo<ContactsListViewModel>();
        }
    }
}