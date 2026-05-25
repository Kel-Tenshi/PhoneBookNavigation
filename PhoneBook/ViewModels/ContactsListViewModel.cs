using PhoneBook.Core;
using PhoneBook.Models;
using PhoneBook.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace PhoneBook.ViewModels
{
    public class ContactsListViewModel : ObservableObject
    {
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;

        // Статический список, чтобы данные не терялись при пересоздании ViewModel
        private static readonly ObservableCollection<Contact> _staticContacts = new ObservableCollection<Contact>();
        public ObservableCollection<Contact> Contacts => _staticContacts;

        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        private string _phone = string.Empty;
        public string Phone
        {
            get => _phone;
            set => Set(ref _phone, value);
        }

        private Contact? _selectedContact;
        public Contact? SelectedContact
        {
            get => _selectedContact;
            set => Set(ref _selectedContact, value);
        }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }

        public ContactsListViewModel(IDialogService dialogService, INavigationService navigationService)
        {
            _dialogService = dialogService;
            _navigationService = navigationService;

            AddCommand = new RelayCommand(AddContact, CanAddContact);
            DeleteCommand = new RelayCommand<Contact>(DeleteContact, CanDeleteContact);
            EditCommand = new RelayCommand<Contact>(EditContact, CanEditContact);
        }

        private void AddContact()
        {
            string digitsOnly = new string(Phone.Where(char.IsDigit).ToArray());

            if (Contacts.Any(c => new string(c.Phone.Where(char.IsDigit).ToArray()) == digitsOnly))
            {
                _dialogService.ShowWarning("Контакт с таким номером уже существует!");
                return;
            }

            var newContact = new Contact(Name, Phone);
            Contacts.Add(newContact);

            _dialogService.ShowInfo($"Контакт {Name} успешно добавлен.");

            Name = string.Empty;
            Phone = string.Empty;
        }

        private bool CanAddContact()
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Phone))
                return false;

            if (Phone.Any(char.IsLetter)) return false;

            string digitsOnly = new string(Phone.Where(char.IsDigit).ToArray());
            return digitsOnly.Length >= 10 && digitsOnly.Length <= 12;
        }

        private void DeleteContact(Contact? contact)
        {
            if (contact != null)
            {
                bool isConfirmed = _dialogService.ShowConfirmation($"Вы действительно хотите удалить контакт {contact.Name}?");
                if (isConfirmed)
                {
                    Contacts.Remove(contact);
                }
            }
        }

        private bool CanDeleteContact(Contact? contact) => contact != null;

        private void EditContact(Contact? contact)
        {
            if (contact != null)
            {
                // Навигация к форме редактирования с передачей выбранного контакта
                _navigationService.NavigateTo<ContactEditViewModel>(contact);
            }
        }

        private bool CanEditContact(Contact? contact) => contact != null;
    }
}