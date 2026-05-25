using PhoneBook.Core;
using PhoneBook.Models;
using PhoneBook.Services;
using System.Windows.Input;

namespace PhoneBook.ViewModels
{
    public class ContactEditViewModel : ObservableObject, INavigationAware
    {
        private readonly INavigationService _navigation;
        private Contact _contact = null!;

        public string EditName
        {
            get => _contact?.Name ?? string.Empty;
            set
            {
                if (_contact != null)
                {
                    _contact.Name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string EditPhone
        {
            get => _contact?.Phone ?? string.Empty;
            set
            {
                if (_contact != null)
                {
                    _contact.Phone = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public ContactEditViewModel(INavigationService navigation)
        {
            _navigation = navigation;

            // Команды перенаправляют обратно на список контактов
            SaveCommand = new RelayCommand(
                () => _navigation.NavigateTo<ContactsListViewModel>());
            CancelCommand = new RelayCommand(
                () => _navigation.NavigateTo<ContactsListViewModel>());
        }

        public void OnNavigatedTo(object? parameter)
        {
            if (parameter is Contact c)
            {
                _contact = c;
                OnPropertyChanged(nameof(EditName));
                OnPropertyChanged(nameof(EditPhone));
            }
        }
    }
}