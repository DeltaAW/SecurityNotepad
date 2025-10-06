using System;
using System.Collections.Generic;
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
     
        public EntryWindow()
        {         
            InitializeComponent();
            checkingForFoldersAndFiles.CreateFoldersForStrongAccounts();
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordPlaceholder.Visibility =
                string.IsNullOrEmpty(PasswordBox.Password) ? Visibility.Visible : Visibility.Hidden; // событие убирает текст в пароле, когда пользователь начинает писать
        }

        private void TextBox_LoginChanged(object sender, RoutedEventArgs e)
        {
            LoginPlaceholder.Visibility =
                string.IsNullOrEmpty(Login.Text) ? Visibility.Visible : Visibility.Hidden; // событие убирает текст в логине, когда пользователь начинает писать
        }

        private void Button_LogIn(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Кнопка работает");
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
    }
}
