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
using OProfTest.MVVM.Model;

namespace OProfTest.MVVM.View
{
    /// <summary>
    /// Interaction logic for UserEditor.xaml
    /// </summary>
    public partial class UserEditor : Window
    {
        public UserEditor(User user)
        {
            InitializeComponent();
            var win = this;
            this.DataContext = new UserEditorViewModel(user, win);
        }

        private void closeWindow(object sender, RoutedEventArgs e)
        {
            App.Current.Windows[0].Show();
            this.Close();
        }
    }
}
