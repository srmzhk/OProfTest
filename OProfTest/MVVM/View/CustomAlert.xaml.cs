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
    /// Interaction logic for CustomAlert.xaml
    /// </summary>
    public partial class CustomAlert : Window
    {
        public CustomAlert()
        {
            InitializeComponent();
        }

        private void backToAuthorizationClick(object sender, RoutedEventArgs e)
        {
            var authorization = new UserAuthorization();
            authorization.Show();
            this.Close();
        }
    }
}
