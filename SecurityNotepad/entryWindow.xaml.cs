using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SecurityNotepad
{
    /// <summary>
    /// Логика взаимодействия для entryWindow.xaml
    /// </summary>
    /// IncorrectPassOrLog.Visibility = Visibility.Visible; для вывода ложного пароля
    /// bool ok = PasswordHasher.Verify("mySuperSecret", stored); // true
    public partial class EntryWindow : Window
    {
        CheckingForFoldersAndFiles checkingForFoldersAndFiles = new();
        AccountManager account = new();
     
        public EntryWindow()
        {         
            InitializeComponent();
            checkingForFoldersAndFiles.CreateFoldersForStrongAccounts();
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e) =>  
            PasswordPlaceholder.Visibility = string.IsNullOrEmpty(PasswordBox.Password) ? Visibility.Visible : Visibility.Hidden; // событие убирает текст в пароле, когда пользователь начинает писать
        

        private void TextBox_LoginChanged(object sender, RoutedEventArgs e) =>        
            LoginPlaceholder.Visibility = string.IsNullOrEmpty(Login.Text) ? Visibility.Visible : Visibility.Hidden; // событие убирает текст в логине, когда пользователь начинает писать

        public void LoginError(string textError) => (LogInError.Text, LogInError.Visibility) = (textError, Visibility.Visible); // вывод ошибки

        public bool AllVerifiacation() => LoginVerification() && PresenceOfPassword(); // проверка на наличие логина и пароля

        public bool LoginVerification() => !string.IsNullOrEmpty(Login.Text);  // проверка на наличие логина

        public bool PresenceOfPassword() => !string.IsNullOrEmpty(PasswordBox.Password); // проверка на наличие пароля

        private void Button_LogIn(object sender, RoutedEventArgs e)
        {
            if (LoginError())
            {
                // Получаем хеш из файла
                string? storedHash = account.GetPasswordHash(Login.Text);

                if (storedHash == null)
                {
                    LoginError("The user was not found");
                    return;
                }

                // Сравниваем введённый пароль с хешем
                bool ok = PasswordHasher.Verify(PasswordBox.Password, storedHash);

                if (ok)
                {
                    MainWindow mW = new();
                    Close();
                    mW.Show();
                }
                else
                {
                    LoginError("Incorrect password.");
                }
            }
        }

        private void Button_SignUp(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new();
            registrationWindow.Show();
            Close();
        }

        private void Button_ExiteButton(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private bool LoginError() 
        {
            if (!AllVerifiacation())
            {
                if (!LoginVerification())
                    LoginError("The login is empty.");
                else if (!PresenceOfPassword())
                    LoginError("The password is empty.");
            }
            return AllVerifiacation();
        }
    }
}
