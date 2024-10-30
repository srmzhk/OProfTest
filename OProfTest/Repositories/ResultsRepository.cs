using OProfTest.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OProfTest.Repositories
{
    internal class ResultsRepository
    {
        private readonly ModelsManager _dbManager;
        public ResultsRepository()
        {
            _dbManager = new ModelsManager();
        }

        public void AddResult(Result newResult)
        {
            newResult.ResultDate = DateTime.Now;
            _dbManager.Results.Add(newResult);
            _dbManager.SaveChanges();
        }

        public void DeleteResultsByUserID(int userID)
        {
            var results = _dbManager.Results.Where(r => r.UserID == userID).ToList();
            foreach (var result in results)
                _dbManager.Results.Remove(result);
            _dbManager.SaveChanges();
        }

        public void DeleteResultsByTestID(int testID)
        {
            var results = _dbManager.Results.Where(r => r.TestID == testID).ToList();
            foreach (var result in results)
                _dbManager.Results.Remove(result);
            _dbManager.SaveChanges();
        }

        public List<Result> GetAllResultsByUserID(int userID)
        {
            return _dbManager.Results.Where(p => p.UserID == userID).ToList();
        }

        public List<Result> GetAllResultsByTestID(int tetsID)
        {
            return _dbManager.Results.Where(p => p.TestID == tetsID).ToList();
        }
    }
}
