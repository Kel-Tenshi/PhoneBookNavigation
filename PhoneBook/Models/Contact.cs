using PhoneBook.Core;
using System;
using System.Text.RegularExpressions;

namespace PhoneBook.Models
{
    /// <summary>
    /// Модель данных.
    /// </summary>
    public class Contact : ObservableObject
    {
        private string _name = string.Empty;
        private string _phone = string.Empty;

        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        public string Phone
        {
            get => _phone;
            set => Set(ref _phone, value);
        }

        public Contact(string name, string phone)
        {
            Name = name;
            Phone = phone;

            if (!Validate())
            {
                throw new ArgumentException("Некорректные данные контакта.");
            }
        }

        /// <summary>
        /// Метод валидации данных бизнес-модели
        /// </summary>
        public bool Validate()
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Phone))
                return false;

            if (Phone.Any(char.IsLetter)) return false;

            string digitsOnly = new string(Phone.Where(char.IsDigit).ToArray());

            return digitsOnly.Length >= 10 && digitsOnly.Length <= 12;
        }
    }
}