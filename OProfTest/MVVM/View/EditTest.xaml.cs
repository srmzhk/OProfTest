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
    /// Interaction logic for EditTest.xaml
    /// </summary>
    public partial class EditTest : Window
    {
        public EditTest()
        {
            InitializeComponent();
            Closing += ToAddTest;
            this.DataContext = new EditTestViewModel();
        }
        private void ToAddTest(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var win = new AdminWindow();
            win.Show();
        }
    }
}
