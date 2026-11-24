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
using System.Security.Principal;

namespace PractWork11
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            LoadWindowsAccountInfo();
        }

        private void LoadWindowsAccountInfo() 
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();

            if (identity != null && identity.IsAuthenticated)
            { 
                string accountName = identity.Name;

                WindowsPrincipal principal = new WindowsPrincipal(identity);
                bool isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);

                string roleText = isAdmin ? "Администратор" : "Обычный пользователь";

                TxtAccountInfo.Text = $"Имя пользователя: {accountName}\nРоль: {roleText}";
            }
            else
            {
                TxtAccountInfo.Text = "Ошибка во время аутентификации";
            }
        }
    }
}