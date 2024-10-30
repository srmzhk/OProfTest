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
using OProfTest.MVVM.ViewModel;

namespace OProfTest.MVVM.View
{
    /// <summary>
    /// Interaction logic for UserRegistration.xaml
    /// </summary>
    public partial class UserRegistration : Window
    {
        public UserRegistration()
        {
            InitializeComponent();
            this.DataContext = new UserRegistrationViewModel();
        }

        private void closeButtonClick(object sender, RoutedEventArgs e)
        {
            var startWin = new StartWindow();
            startWin.Show();
            this.Close();
        }

        private void backToAuthorizationClick(object sender, RoutedEventArgs e)
        {
            var authorization = new UserAuthorization();
            authorization.Show();
            this.Close();
        }
    }
}
