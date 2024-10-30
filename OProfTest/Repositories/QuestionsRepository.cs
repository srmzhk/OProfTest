using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OProfTest.MVVM.Model;

namespace OProfTest.Repositories
{
    internal class QuestionsRepository
    {
        private readonly ModelsManager _dbManager;
        public QuestionsRepository()
        {
            _dbManager = new ModelsManager();
        }

        public void AddQuestion(Question newQuestion)
        {
            _dbManager.Questions.Add(newQuestion);
            _dbManager.SaveChanges();
        }

        public void DeleteQuestionById(int questionID)
        {
            _dbManager.Questions.Remove(GetQuestionById(questionID));
            _dbManager.SaveChanges();
        }

        public void DeleteQuestionsByTestID(int testID)
        {
            var questions = _dbManager.Questions.Where(q => q.TestID == testID).ToList();
            foreach (var question in questions)
                _dbManager.Questions.Remove(question);
            _dbManager.SaveChanges();
        }

        public List<Question> GetAllQuestions()
        {
            return _dbManager.Questions.ToList();
        }

        public List<Question> GetAllQuestionsByTestID(int testID)
        {
            return _dbManager.Questions.Where(p => p.TestID == testID).ToList();
        }

        public Question GetQuestionById(int QuestionId)
        {
            return _dbManager.Questions.Find(QuestionId);
        }

        public void UpdateQuestion(Question changedQuestion)
        {
            var question = _dbManager.Questions.Find(changedQuestion.ID);
            question.Title = changedQuestion.Title;
            _dbManager.Entry(question).State = EntityState.Modified;
            _dbManager.SaveChanges();
        }
    }
}
