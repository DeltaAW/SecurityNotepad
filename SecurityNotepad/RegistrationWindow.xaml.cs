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
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }
        private void PasswordBox_RegisterPassword(object sender, RoutedEventArgs e)
        {
            RegisterPassword.Visibility = string.IsNullOrEmpty(PasswordRegister.Password) ? Visibility.Visible : Visibility.Hidden;
        }


        private void TextBox_RegisterLogin(object sender, RoutedEventArgs e)
        {
            LoginBox.Visibility =
                string.IsNullOrEmpty(LoginBox.Text) ? Visibility.Visible : Visibility.Hidden; // событие убирает текст в логине, когда пользователь начинает писать
        }
        public void Button_BackButton(object sender, RoutedEventArgs e)
        {
            EntryWindow entryWindow = new();
            Close();
            entryWindow.Show();
        }  
        public void Button_RegisterButton(object sender, RoutedEventArgs e)
        {

        }
        public void Button_ExiteButton(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
