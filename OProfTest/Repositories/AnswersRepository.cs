using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OProfTest.MVVM.Model;

namespace OProfTest.Repositories
{
    internal class AnswersRepository
    {
        private readonly ModelsManager _dbManager;
        public AnswersRepository()
        {
            _dbManager = new ModelsManager();
        }

        public List<Answer> GetAllAnswersByTestID(int id)
        {
            return _dbManager.Answers.Where(p => p.TestID == id).ToList();
        }

        public List<Answer> GetAllAnswersByQuestion(int qt, int testID)
        {
            return _dbManager.Answers.Where(p => p.AnswerType == qt && p.TestID == testID).ToList();
        }
        public List<Answer> GetAllAnswersByType(int type, int testID)
        {
            return _dbManager.Answers.Where(p => p.AnswerType == type && p.TestID == testID).ToList();
        }


        public Answer GetAnswerById(int answerId)
        {
            return _dbManager.Answers.Find(answerId);
        }
        public void DeleteAnswersByTestID(int id)
        {
            var answs = _dbManager.Answers.Where(p => p.TestID == id).ToList();
            foreach (var answer in answs)
            {
                if (answer != null)
                    _dbManager.Answers.Remove(answer);
            }
            _dbManager.SaveChanges();
        }
    }
}
