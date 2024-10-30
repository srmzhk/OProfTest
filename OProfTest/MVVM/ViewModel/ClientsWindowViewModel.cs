using OProfTest.MVVM.Model;
using OProfTest.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OProfTest.MVVM.View;

namespace OProfTest.MVVM.ViewModel
{
    internal class ClientsWindowViewModel : ObservableObject
    {
        private readonly UsersRepository _usersRepository;

        private User _selectedUser;

        public ObservableCollection<User> Users { get; set; }


        public ClientsWindowViewModel()
        {
            _usersRepository = new UsersRepository();
            Users = new ObservableCollection<User>();
            _selectedUser = new User();
            LoadUsers();
        }

        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged("SelectedUser");
            }
        }

        private void LoadUsers()
        {
            var users = _usersRepository.GetAllUsers();
            Users.Clear();
            foreach (var user in users)
                Users.Add(user);
        }

        private readonly RelayCommand _deleteUser;
        public RelayCommand DeleteUser
        {
            get
            {
                return _deleteUser ?? (new RelayCommand(obj =>
                {
                    try
                    {
                        if (_selectedUser.ID == 0)
                            MessageBox.Show("Выберите ID пользователя!");
                        else
                        {
                            _usersRepository.DeleteUser(_selectedUser.ID);
                            MessageBox.Show("Удаление пользователя ID:" + _selectedUser.ID + " со всем связующими выполнено успешно!");
                            LoadUsers();
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show("Произошла ошибка!" + ex.Message);
                    }
                }));
            }
        }

        private readonly RelayCommand _changeUser;
        public RelayCommand ChangeUser
        {
            get
            {
                return _changeUser ?? (new RelayCommand(obj =>
                {
                    try
                    {
                        if (_selectedUser.ID == 0)
                            MessageBox.Show("Выберите ID пользователя!");
                        else
                        {
                            var win = new UserEditor(_selectedUser);
                            App.Current.Windows[0].Hide();
                            win.Show();
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show("Произошла ошибка!" + ex.Message);
                    }
                }));
            }
        }
    }
}
