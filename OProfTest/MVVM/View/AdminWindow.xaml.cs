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
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void DeleteWindowClick(object sender, RoutedEventArgs e)
        {
            var win = new DeleteTest();
            App.Current.Windows[0].Close();
            win.Show();
        }

        private void AddWindowClick(object sender, RoutedEventArgs e)
        {
            var win = new AddNewTest();
            App.Current.Windows[0].Close();
            win.Show();
        }

        private void EditorWindowClick(object sender, RoutedEventArgs e)
        {
            var win = new EditTest();
            App.Current.Windows[0].Close();
            win.Show();
        }

        private void toClientsWindow(object sender, RoutedEventArgs e)
        {
            var win = new ClientsWindow();
            App.Current.Windows[0].Close();
            win.Show();
        }

        private void toResultsClick(object sender, RoutedEventArgs e)
        {
            var win = new UserResults();
            App.Current.Windows[0].Close();
            win.Show();
        }
    }
}
