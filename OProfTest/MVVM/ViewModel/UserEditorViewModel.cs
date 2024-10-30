using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using HashGenerators;
using OProfTest.MVVM.Model;
using OProfTest.MVVM.View;
using OProfTest.Repositories;

namespace OProfTest.MVVM.ViewModel
{
    internal class UserEditorViewModel : ObservableObject
    {
        private readonly UsersRepository _usersRepository;
        private readonly Regex _regexForUserLogin;
        private readonly Regex _regexForUserName;
        private readonly Regex _regexForUserAge;

        private User _user;
        private UserEditor _win;

        public UserEditorViewModel(User changedUser, UserEditor win)
        {
            _usersRepository = new UsersRepository();
            _regexForUserLogin = new Regex("^[a-zA-Z0-9]+$");
            _regexForUserName = new Regex(@"^[a-zA-Zа-яА-Я]+$");
            _regexForUserAge = new Regex(@"^\d+$");
            _user = new User();
            _user.ID = changedUser.ID;
            _user.Password = changedUser.Password;
            _user.Login = changedUser.Login;
            _user.FirstName = changedUser.FirstName;
            _user.LastName = changedUser.LastName;
            _user.Age = changedUser.Age;
            _user.Role = changedUser.Role;
            _user.RegistrationDate = changedUser.RegistrationDate;
            _win = win;
            OnWindowLoad();
        }

        private void OnWindowLoad()
        {
            FirstName = _user.FirstName;
            LastName = _user.LastName;
            Age = _user.Age;
            Login = _user.Login;
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
        private int _age;
        public int Age
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

        private readonly RelayCommand _changeUser;
        public RelayCommand ChangeUser
        {
            get
            {

                return _changeUser ?? (new RelayCommand(async obj =>
                {
                    try
                    {
                        if (!_regexForUserName.IsMatch(FirstName) || String.IsNullOrWhiteSpace(FirstName))
                            MessageBox.Show("Введите корректно Имя.");
                        else if (!_regexForUserName.IsMatch(LastName) || String.IsNullOrWhiteSpace(LastName))
                            MessageBox.Show("Введите корректно Фамилию.");
                        else if (!_regexForUserAge.IsMatch(Age.ToString()) || String.IsNullOrWhiteSpace(Age.ToString()))
                            MessageBox.Show("Введите корректно Возраст.");
                        else if (!_regexForUserLogin.IsMatch(Login) || String.IsNullOrWhiteSpace(Login))
                            MessageBox.Show("Введите корректно Логин.");
                        else
                        {
                            _user.FirstName = _firstName;
                            _user.LastName = _lastName;
                            _user.Age = _age;
                            _user.Login = _login;
                            if (Password1 != null)
                                _user.Password = MD5Generator.ProduceMD5Hash(Password1);
                            _usersRepository.UpdateUser(_user);
                            MessageBox.Show("Участник успешно изменён!");
                            _win.Close();
                            App.Current.Windows[0].Show();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Необходимо заполнить все поля!\n" + ex.Message + ex.StackTrace);
                    }
                }));
            }
        }
    }
}