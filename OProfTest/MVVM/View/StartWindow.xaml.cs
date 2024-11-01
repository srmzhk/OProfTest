﻿using OProfTest.MVVM.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OProfTest.MVVM.View
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
        }

        private void cancelButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void toAuthorizationClick(object sender, RoutedEventArgs e)
        {
            var authorization = new UserAuthorization();
            this.Close();
            authorization.Show();
        }
    }
}