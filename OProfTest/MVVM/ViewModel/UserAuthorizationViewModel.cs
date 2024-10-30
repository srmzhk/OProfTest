using HashGenerators;
using OProfTest.MVVM.Model;
using OProfTest.MVVM.View;
using OProfTest.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace OProfTest.MVVM.ViewModel
{
    internal class UserAuthorizationViewModel : ObservableObject
    {
        private readonly UsersRepository _usersRepository;

        private readonly Regex _regexForUserLogin;

        public UserAuthorizationViewModel(){
            _usersRepository = new UsersRepository();
            _regexForUserLogin = new Regex("^[a-zA-Z0-9]+$");
        }
        private string _login;
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }
        private string _password1;
        public string Password1
        {
            get { return _password1; }
            set
            {
                _password1 = value;
                OnPropertyChanged(nameof(Password1));
            }
        }
        private readonly RelayCommand _authorization;
        public RelayCommand Authorization
        {
            get
            {
                return _authorization ?? (new RelayCommand(async obj =>
                {
                    try
                    {
                        if (!_regexForUserLogin.IsMatch(Login) || String.IsNullOrWhiteSpace(Login))
                            MessageBox.Show("Введите корректно Логин.");
                        else if (String.IsNullOrWhiteSpace(Password1))
                            MessageBox.Show("Введите корректно Пароль.");
                        User user = _usersRepository.FindUserByLogin(Login);
                        if (user != null)
                        {
                            string pass = MD5Generator.ProduceMD5Hash(Password1);
                            // Admin or user
                            if(user.Password == pass && user.Login == Login && user.Role == 1)
                            {
                                //Succes admin
                                var adminWin = new AdminWindow();
                                App.Current.Windows[0].Close();
                                adminWin.Show();
                            }
                            else if (user.Password == pass && user.Login == Login)
                            {
                                // Success user
                                var testsCatalog = new TestsCatalog(user);
                                App.Current.Windows[0].Close();
                                testsCatalog.Show();
                            }
                            else
                            {
                                MessageBox.Show("Неверный пароль. Попробуйте ещё раз.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Неверный логин. Попробуйте ещё раз.");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Необходимо вести все значение!");
                    }
                }));
            }
        }
    }
}
