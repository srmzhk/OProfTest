using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OProfTest.MVVM.Model;

namespace OProfTest.Repositories
{
    public class UsersRepository
    {
        private readonly ModelsManager _dbManager;
        public UsersRepository()
        {
            _dbManager = new ModelsManager();
        }
        public async Task<int> AddUser(User newUser)
        {
            _dbManager.Users.Add(newUser);
            return await _dbManager.SaveChangesAsync();
        }

        public void DeleteUser(int userId)
        {
            var user = _dbManager.Users.Find(userId);
            if (user != null)
                _dbManager.Users.Remove(user);
            _dbManager.SaveChanges();
        }

        public User FindUserByLogin(string login)
        {
            return _dbManager.Users.Where(user => user.Login == login).FirstOrDefault();
        }

        public List<User> GetAllUsers()
        {
            return _dbManager.Users.Where(user => user.Role == 2).ToList();
        }

        public void UpdateUser(User changedUser)
        {
            var user = _dbManager.Users.Find(changedUser.ID);
            if (user != null)
            {
                _dbManager.Users.AddOrUpdate(changedUser);
                _dbManager.SaveChanges();
            }
        }
    }
}
