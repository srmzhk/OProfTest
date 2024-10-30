using OProfTest.MVVM.ViewModel;
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

namespace OProfTest.MVVM.View
{
    /// <summary>
    /// Interaction logic for UserAuthorization.xaml
    /// </summary>
    public partial class UserAuthorization : Window
    {
        public UserAuthorization()
        {
            InitializeComponent();
            this.DataContext = new UserAuthorizationViewModel();
        }

        private void closeClick(object sender, RoutedEventArgs e)
        {
            var startWin = new StartWindow();
            startWin.Show();
            this.Close();
        }

        private void backToRegisterClick(object sender, RoutedEventArgs e)
        {
            var registration = new UserRegistration();
            registration.Show();
            this.Close();
        }
    }
}
