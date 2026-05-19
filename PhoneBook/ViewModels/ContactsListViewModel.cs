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
        // Поле для хранения зависимости (сервиса)
        private readonly IDialogService _dialogService;

        public ObservableCollection<Contact> Contacts { get; }

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

        // Внедрение зависимости через конструктор (Constructor Injection).
        // Контейнер DI автоматически подставит реализацию (DialogService) при создании ViewModel.
        public ContactsListViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            Contacts = new ObservableCollection<Contact>();

            AddCommand = new RelayCommand(AddContact, CanAddContact);
            DeleteCommand = new RelayCommand<Contact>(DeleteContact, CanDeleteContact);
        }

        private void AddContact()
        {
            // Извлекаем только цифры из введенного телефона для корректного сравнения
            string digitsOnly = new string(Phone.Where(char.IsDigit).ToArray());

            // 1. Проверка на дубликаты
            if (Contacts.Any(c => new string(c.Phone.Where(char.IsDigit).ToArray()) == digitsOnly))
            {
                _dialogService.ShowWarning("Контакт с таким номером уже существует!");
                return;
            }

            // 2. Добавление
            var newContact = new Contact(Name, Phone);
            Contacts.Add(newContact);

            // 3. Информирование об успехе
            _dialogService.ShowInfo($"Контакт {Name} успешно добавлен.");

            // Очистка полей ввода
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
                // Запрос подтверждения перед удалением
                bool isConfirmed = _dialogService.ShowConfirmation($"Вы действительно хотите удалить контакт {contact.Name}?");
                if (isConfirmed)
                {
                    Contacts.Remove(contact);
                }
            }
        }

        private bool CanDeleteContact(Contact? contact) => contact != null;
    }
}