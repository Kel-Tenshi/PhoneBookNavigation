using PhoneBook.Core;

namespace PhoneBook.ViewModels
{
    public class AboutViewModel : ObservableObject
    {
        public string AppName => "Телефонная книга MVVM";
        public string Version => "Версия 2.0";
    }
}