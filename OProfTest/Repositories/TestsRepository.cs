using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OProfTest.MVVM.Model;

namespace OProfTest.Repositories
{
    public class TestsRepository
    {
        private readonly ModelsManager _dbManager;
        public TestsRepository()
        {
            _dbManager = new ModelsManager();
        }
        public void AddNewTest(Test Test)
        {
            _dbManager.Tests.Add(Test);
            _dbManager.SaveChanges();
        }

        public void DeleteTestById(int TestId)
        {
            _dbManager.Tests.Remove(GetTestById(TestId));
            _dbManager.SaveChanges();
        }

        public List<Test> GetAllTests()
        {
            return _dbManager.Tests.ToList();
        }

        public Test GetTestById(int TestId)
        {
            return _dbManager.Tests.Find(TestId);
        }

        public Test GetTestByTitle(string TestTitle)
        {
            return _dbManager.Tests.Where(p => p.Title.Equals(TestTitle)).FirstOrDefault();
        }

        public void UpdateTest(Test changedTest)
        {
            var Test = _dbManager.Tests.Find(changedTest.ID);
            Test.Title = changedTest.Title;
            Test.Description = changedTest.Description;
            Test.TestImages = changedTest.TestImages;
            _dbManager.Entry(Test).State = EntityState.Modified;
            _dbManager.SaveChanges();
        }
    }
}