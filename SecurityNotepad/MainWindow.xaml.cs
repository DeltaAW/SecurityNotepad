using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Size(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
        }
        private void Button_Exite(object sender, RoutedEventArgs e) => Close();
        private void Button_Minimize(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        // Событие MouseDown для Canvas (или панели заголовка)
        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
        private void Button_File(object sender, RoutedEventArgs e)
        {

        }
        private void Button_Account(object sender, RoutedEventArgs e)
        {

        }
        private void Button_Print(object sender, RoutedEventArgs e)
        {

        }
    }
}