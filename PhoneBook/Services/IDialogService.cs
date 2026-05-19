namespace PhoneBook.Services
{
    /// <summary>
    /// Интерфейс для абстрагирования работы с диалоговыми окнами.
    /// Позволяет ViewModel общаться с пользователем, не зная о UI-классах WPF.
    /// </summary>
    public interface IDialogService
    {
        void ShowInfo(string message, string title = "Информация");
        void ShowWarning(string message, string title = "Предупреждение");
        void ShowError(string message, string title = "Ошибка");
        bool ShowConfirmation(string message, string title = "Подтверждение");
    }
}