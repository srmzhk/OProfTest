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
using OProfTest.MVVM.Model;
using OProfTest.MVVM.ViewModel;

namespace OProfTest.MVVM.View
{
    /// <summary>
    /// Interaction logic for ResultWindow.xaml
    /// </summary>
    public partial class ResultWindow : Window
    {
        public ResultWindow(Result result, User user)
        {
            InitializeComponent();
            this.DataContext = new ResultWindowViewModel(result, user);
        }
    }
}
