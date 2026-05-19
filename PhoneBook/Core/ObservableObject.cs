using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PhoneBook.Core
{
    /// <summary>
    /// Базовый класс, реализующий интерфейс INotifyPropertyChanged.
    /// </summary>
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        // Метод для вызова события изменения свойства
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Вспомогательный метод для установки значения и автоматического вызова OnPropertyChanged
        protected bool Set<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}