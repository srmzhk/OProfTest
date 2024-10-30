using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OProfTest.MVVM.Model;
using OProfTest.MVVM.View;
using OProfTest.Repositories;

namespace OProfTest.MVVM.ViewModel
{
    internal class UserResultsViewModel : ObservableObject
    {
        private readonly UsersRepository _usersRepository;
        private readonly TestsRepository _testsRepository;
        private readonly ResultsRepository _resultsRepository;

        private User _selectedUser;

        public ObservableCollection<User> Users { get; set; }
        public ObservableCollection<Result> Results { get; set; }
        public ObservableCollection<Test> Tests { get; set; }

        public UserResultsViewModel()
        {
            _usersRepository = new UsersRepository();
            _testsRepository = new TestsRepository();
            _resultsRepository = new ResultsRepository();

            Users = new ObservableCollection<User>();
            Results = new ObservableCollection<Result>();
            Tests = new ObservableCollection<Test>();
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
                LoadResultsByUserID(_selectedUser.ID);
            }
        }

        private void LoadUsers()
        {
            var users = _usersRepository.GetAllUsers();
            Users.Clear();
            foreach (var user in users)
                Users.Add(user);
        }
        private void LoadResultsByUserID(int userID)
        {
            var results = _resultsRepository.GetAllResultsByUserID(userID);
            Results.Clear();
            Tests.Clear();
            foreach(var result in results)
            {
                Results.Add(result);
                Tests.Add(_testsRepository.GetTestById(result.TestID));
            }
        }
    }
}
