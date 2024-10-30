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
    /// Interaction logic for SocionalTypeTest.xaml
    /// </summary>
    public partial class SocionalTypeTest : Window
    {
        private User _currentUser;
        public SocionalTypeTest(User user, Test test)
        {
            _currentUser = user;
            InitializeComponent();
            this.DataContext = new SocionalTypeTestViewModel(user, test);
        }

        private void backToTests(object sender, MouseButtonEventArgs e)
        {
            if (CustomMessageBox("Вы уверены, что хотите завершить тест?", "Подтверждение", "Да", "Нет") == MessageBoxResult.Yes)
            {
                var win = new TestsCatalog(_currentUser);
                this.Close();
                win.Show();
            }
        }

        MessageBoxResult CustomMessageBox(string message, string title, string yesButtonText, string noButtonText)
        {
            // Создаем новое окно
            Window window = new Window()
            {
                Title = title,
                Width = 300,
                Height = 150,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                ResizeMode = ResizeMode.NoResize,
                WindowStyle = WindowStyle.ToolWindow,
                Content = new StackPanel
                {
                    Margin = new Thickness(20),
                    Children =
            {
                new TextBlock { Text = message, Margin = new Thickness(0, 0, 0, 10) },
                new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Children =
                    {
                        new Button { Content = yesButtonText, Margin = new Thickness(0, 0, 10, 0), Width = 80, Tag = MessageBoxResult.Yes},
                        new Button { Content = noButtonText, Width = 80, Tag = MessageBoxResult.No}
                    }
                }
            }
                }
            };

            // Обрабатываем клик на кнопку
            MessageBoxResult result = MessageBoxResult.None;
            foreach (Button button in ((StackPanel)((StackPanel)window.Content).Children[1]).Children)
            {
                button.Click += (sender, e) =>
                {
                    result = (MessageBoxResult)((Button)sender).Tag;
                    window.Close();
                };
            }

            // Отображаем окно и возвращаем результат
            window.ShowDialog();
            return result;
        }
    }
}
