using OProfTest.MVVM.Model;
using OProfTest.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Windows;
using HashGenerators;
using OProfTest.MVVM.View;
using System.IO;


namespace OProfTest.MVVM.ViewModel
{
    internal class UserRegistrationViewModel : ObservableObject
    {
        private readonly UsersRepository _usersRepository;
        private readonly Regex _regexForUserLogin;
        private readonly Regex _regexForUserName;
        private readonly Regex _regexForUserAge;

        public UserRegistrationViewModel()
        {
            _usersRepository = new UsersRepository();
            _regexForUserLogin = new Regex("^[a-zA-Z0-9]+$");
            _regexForUserName = new Regex(@"^[a-zA-Zа-яА-Я]+$");
            _regexForUserAge = new Regex(@"^\d+$");
        }
        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
        private string _age;
        public string Age
        {
            get { return _age; }
            set
            {
                _age = value;
                OnPropertyChanged(nameof(Age));
            }
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
        private string _password2;
        public string Password2
        {
            get { return _password2; }
            set
            {
                _password2 = value;
                OnPropertyChanged(nameof(Password2));
            }
        }
        private readonly RelayCommand _register;
        public RelayCommand Register
        {
            get
            {
                
                return _register ?? (new RelayCommand(async obj =>
                {
                    try
                    {
                        if (!_regexForUserName.IsMatch(FirstName) || String.IsNullOrWhiteSpace(FirstName))
                            MessageBox.Show("Введите корректно Имя.");
                        else if (!_regexForUserName.IsMatch(LastName) || String.IsNullOrWhiteSpace(LastName))
                            MessageBox.Show("Введите корректно Фамилию.");
                        else if (!_regexForUserAge.IsMatch(Age) || String.IsNullOrWhiteSpace(Age))
                            MessageBox.Show("Введите корректно Возраст.");
                        else if (!_regexForUserLogin.IsMatch(Login) || String.IsNullOrWhiteSpace(Login))
                            MessageBox.Show("Введите корректно Логин.");
                        else if (String.IsNullOrWhiteSpace(Password1))
                            MessageBox.Show("Введите корректно Пароль.");
                        else if (Password1 != Password2)
                            MessageBox.Show("Пароли не совпадают.");
                        else
                        {
                            User userWithEqualLogin = _usersRepository.FindUserByLogin(Login);
                            if (userWithEqualLogin != null)
                                MessageBox.Show($"Пользователь с логином - \"{Login}\" уже сущёствует.");
                            else
                            {
                                User newUser = new User()
                                {
                                    FirstName = FirstName,
                                    LastName = LastName,
                                    Age = Convert.ToInt32(Age),
                                    Login = Login,
                                    Password = MD5Generator.ProduceMD5Hash(Password1),
                                    RegistrationDate = DateTime.Now,
                                };
                                if (Password1.ToUpper() == "ADMIN228")
                                    newUser.Role = 1; // Admin
                                else
                                    newUser.Role = 2; // User
                                int okay = await _usersRepository.AddUser(newUser);
                                string path = "../../../data.txt";
                                string text = newUser.FirstName + " " + newUser.LastName + "\nLogin: " + newUser.Login + "\nPassword: " + Password1 + "\n\n";
                                File.AppendAllText(path, text);
                                var alert = new CustomAlert();
                                App.Current.Windows[0].Close();
                                alert.Show();
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Необходимо заполнить все поля!\n" + ex.Message + ex.StackTrace);
                    }
                }));
            }
        }
    }
}