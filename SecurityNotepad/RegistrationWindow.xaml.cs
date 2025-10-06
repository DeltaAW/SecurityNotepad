using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SecurityNotepad
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// RegisterError.Text = "Пароли не совпадают" - пример вывода ошибки
    /// IncorrectPassOrLog.Visibility = Visibility.Visible; для вывода ложного пароля
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow() => InitializeComponent();

        private void PasswordBox_RegisterPassword(object sender, RoutedEventArgs e) => 
            RegisterPassword.Visibility = string.IsNullOrEmpty(PasswordRegister.Password) ? Visibility.Visible : Visibility.Hidden; // убирает текст в пароле, когда пользователь начинает писать

        private void PasswordBox_RegisterPasswordReplay(object sender, RoutedEventArgs e) => 
            RegisterPasswordReplay.Visibility = string.IsNullOrEmpty(PasswordRegisterReplay.Password) ? Visibility.Visible : Visibility.Hidden; // убирает текст в пароле, когда пользователь начинает писать

        private void TextBox_RegisterLogin(object sender, RoutedEventArgs e) =>
            LoginBox.Visibility = string.IsNullOrEmpty(BackLogin.Text) ? Visibility.Visible : Visibility.Hidden; // убирает текст в логине, когда пользователь начинает писать

        public bool AllVerification() => PasswordVerification() && LoginVerification() && PresenceOfPassword() && PasswordLength(); // проверка на совпадение паролей и наличие логина и пароля и длину пароля
        public bool PasswordVerification() => PasswordRegister.Password == PasswordRegisterReplay.Password; // проверка на совпадение паролей
        public bool LoginVerification() => !string.IsNullOrEmpty(BackLogin.Text);  // проверка на наличие логина
        public bool PasswordLength() => PasswordRegister.Password.Length >= 6;  //проверка на длину пароля
        public bool PresenceOfPassword() => !string.IsNullOrEmpty(PasswordRegister.Password); // проверка на наличие пароля

        public void RegistrationError(string textError) => (RegisterError.Text, RegisterError.Visibility) = (textError, Visibility.Visible); // вывод ошибки

        public void Button_BackButton(object sender, RoutedEventArgs e)
        {
            EntryWindow entryWindow = new();
            Close();
            entryWindow.Show();
        }
        public void Button_RegisterButton(object sender, RoutedEventArgs e)
        {
            if (ValidateRegistration())
            {
                var manager = new AccountManager(); // менеджер аккаунтов

                if (!manager.IsLoginAvailable(BackLogin.Text))
                {
                    // логин уже существует
                    RegistrationError("This login is already taken.");
                    return;
                }

                // хэшируем пароль
                var PasswordHash = PasswordHasher.Hash(PasswordRegister.Password);

                // создаём пользователя
                UserAccount user = new UserAccount(BackLogin.Text, PasswordHash);

                // сохраняем через менеджер
                if (manager.AddAccount(user))
                {
                    // возвращаемся на окно входа
                    EntryWindow entryWindow = new();
                    Close();
                    entryWindow.Show();
                }
                else
                {
                    RegistrationError("Error while saving user.");
                }
            }
        }
        public void Button_ExiteButton(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public bool ValidateRegistration()
        {
            RegisterError.Visibility = Visibility.Hidden;
            if (!AllVerification())
            {
                if (!LoginVerification())
                    RegistrationError("The login is empty.");
                else if (!PresenceOfPassword())
                    RegistrationError("The password is empty.");
                else if (!PasswordLength())
                    RegistrationError("Password must exceed 6 characters.");
                else if (!PasswordVerification())
                    RegistrationError("Password don't match.");
            }
            return AllVerification();
        }
    }
}
