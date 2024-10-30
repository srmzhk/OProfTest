using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OProfTest.MVVM.Model;
using OProfTest.MVVM.View;
using System.Windows.Forms;
using OProfTest.Repositories;
using System.Collections.ObjectModel;
using HashGenerators;
using System.CodeDom;
using System.Threading;
using System.IO;
using Microsoft.Office.Interop.Word;

namespace OProfTest.MVVM.ViewModel
{
    internal class BrigsTestViewModel : ObservableObject
    {
        private User _currentUser;
        private Test _currentTest;
        private Answer _selectedAnswer;
        private Question _currentQuestion;

        private readonly QuestionsRepository _questionsRepository;
        private readonly AnswersRepository _answersRepository;

        public ObservableCollection<Question> Questions { get; set; }
        public ObservableCollection<Answer> Answers { get; set; }

        private Dictionary<string, int> scores;
        private string strToWrite;

        public BrigsTestViewModel(User user, Test test)
        {
            _questionsRepository = new QuestionsRepository();
            _answersRepository = new AnswersRepository();

            Questions = new ObservableCollection<Question>();
            Answers = new ObservableCollection<Answer>();

            _currentUser = user;
            _currentTest = test;
            _selectedAnswer = new Answer();

            scores = new Dictionary<string, int>();
            strToWrite = "";

            InsertValues();
            LoadQuestions();
            LoadAnswersByQuestionType(Questions[0].QuestionType);

            CompletedQuestionsCount = 0;
            _currentQuestion = Questions[CompletedQuestionsCount];
            TestTitle = _currentTest.Title;
            QuestionTitle = _currentQuestion.Title;
            AmountOfQuestions = Questions.Count;
            State = "Вы ответили на " + _completedQuestionsCount + " из " + _amountOfQuestions + " вопросов";
        }

        public Answer SelectedAnswer
        {
            get => _selectedAnswer;
            set
            {
                _selectedAnswer = value;
                try
                {
                    if (value != null)
                    {
                        //checking answer, adding values
                        if (scores.ContainsKey(_selectedAnswer.ValueKey))
                        {
                            if (_selectedAnswer.Value > 0)
                                scores[_selectedAnswer.ValueKey] += _selectedAnswer.Value;
                        }
                        else
                        {
                            throw new Exception("ой-ёй");
                        }

                        CompletedQuestionsCount++;
                        if (CompletedQuestionsCount != _amountOfQuestions)
                        {
                            _currentQuestion = Questions[CompletedQuestionsCount];
                            QuestionTitle = _currentQuestion.Title;
                            LoadAnswersByQuestionType(_currentQuestion.QuestionType);
                        }
                        else
                        {
                            foreach (var score in scores)
                            {
                                strToWrite += score.Key + ": " + score.Value + "\n";
                            }
                            string path = MergeDocs();
                            Result result = new Result();
                            result.TestID = _currentTest.ID;
                            result.UserID = _currentUser.ID;
                            result.FilePath = path;
                            result.Description = strToWrite;
                            var win = new ResultWindow(result, _currentUser);
                            App.Current.Windows[0].Close();
                            win.Show();
                        }
                        _selectedAnswer = null;
                        Thread.Sleep(200);
                    }
                    OnPropertyChanged("SelectedAnswer");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Что-то пошло не так...");
                }
            }
        }

        private string MergeDocs()
        {
            string path = Path.GetFullPath(@"../../../tests/brigs/");
            string letters = "";

            if (scores.ElementAt(0).Value > scores.ElementAt(1).Value)
                letters += scores.ElementAt(0).Key;
            else
                letters += scores.ElementAt(1).Key;

            if (scores.ElementAt(2).Value > scores.ElementAt(3).Value)
                letters += scores.ElementAt(2).Key;
            else
                letters += scores.ElementAt(3).Key;

            if (scores.ElementAt(4).Value > scores.ElementAt(5).Value)
                letters += scores.ElementAt(4).Key;
            else
                letters += scores.ElementAt(5).Key;

            if (scores.ElementAt(6).Value > scores.ElementAt(7).Value)
                letters += scores.ElementAt(6).Key;
            else
                letters += scores.ElementAt(7).Key;

            string filePath1 = Path.GetFullPath(path + letters + ".docx");

            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
            wordApp.Visible = false;
            Microsoft.Office.Interop.Word.Document sourceDoc1 = wordApp.Documents.Open(filePath1);
            Microsoft.Office.Interop.Word.Document targetDoc = wordApp.Documents.Add();
            Microsoft.Office.Interop.Word.Range endRange = targetDoc.Content;
            endRange.Collapse(Microsoft.Office.Interop.Word.WdCollapseDirection.wdCollapseEnd);

            //изменение полей страницы
            var pageSetup = targetDoc.PageSetup;
            float cmToPoints = 28.35f; // Коэффициент для преобразования сантиметров в пункты (точки)
            pageSetup.LeftMargin = 1.5f * cmToPoints; // Левое поле (2.5 см)
            pageSetup.RightMargin = 1f * cmToPoints; // Правое поле (2.5 см)
            pageSetup.TopMargin = 1f * cmToPoints; // Верхнее поле (2.0 см)
            pageSetup.BottomMargin = 1f * cmToPoints; // Нижнее поле (2.0 см)

            //add new paragraph for user data
            endRange.InsertAfter(_currentUser.LastName + " " + _currentUser.FirstName + "\n\n");
            Font font = endRange.Font;
            font.Size = 14;      // Новый размер шрифта
            font.Bold = 1;       // Жирный шрифт (1 - true, 0 - false)
            endRange.Collapse(Microsoft.Office.Interop.Word.WdCollapseDirection.wdCollapseEnd);

            // Copy the content from the source document to the target document
            foreach (Microsoft.Office.Interop.Word.Range range in sourceDoc1.StoryRanges)
            {
                range.Copy();
                endRange.Paste();
            }

            // Закрыть и сохранить исходный документ
            string targetPath = Path.GetFullPath(@"../../../results/" + _currentUser.LastName + "-Бриггс-" + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + ".docx");
            targetDoc.SaveAs2(targetPath);
            targetDoc.Close();
            // Закрыть источник документа
            sourceDoc1.Close();

            // Закрыть приложение Word
            wordApp.Quit();
            return targetPath;
        }

        private void InsertValues()
        {
            scores.Add("E", 0);
            scores.Add("I", 0);
            scores.Add("S", 0);
            scores.Add("N", 0);
            scores.Add("T", 0);
            scores.Add("F", 0);
            scores.Add("J", 0);
            scores.Add("P", 0);
        }

        private void LoadQuestions()
        {
            Questions.Clear();
            var questions = _questionsRepository.GetAllQuestionsByTestID(_currentTest.ID);
            foreach (var question in questions)
            {
                Questions.Add(question);
            }
        }
        private void LoadAnswersByQuestionType(int qt)
        {
            Answers.Clear();
            var answers = _answersRepository.GetAllAnswersByQuestion(qt, _currentTest.ID);
            foreach (var answer in answers)
            {
                Answers.Add(answer);
            }
            _selectedAnswer = null;
        }

        private int _amountOfQuestions;
        public int AmountOfQuestions
        {
            get { return _amountOfQuestions; }
            set
            {
                _amountOfQuestions = value;
                OnPropertyChanged(nameof(AmountOfQuestions));
            }
        }
        private int _completedQuestionsCount;
        public int CompletedQuestionsCount
        {
            get { return _completedQuestionsCount; }
            set
            {
                _completedQuestionsCount = value;
                OnPropertyChanged(nameof(CompletedQuestionsCount));
                State = "Вы ответили на " + _completedQuestionsCount + " из " + _amountOfQuestions + " вопросов";
            }
        }
        private string _testTitle;
        public string TestTitle
        {
            get { return _testTitle; }
            set
            {
                _testTitle = value;
                OnPropertyChanged(nameof(TestTitle));
            }
        }
        private string _questionTitle;
        public string QuestionTitle
        {
            get { return _questionTitle; }
            set
            {
                _questionTitle = value;
                OnPropertyChanged(nameof(QuestionTitle));
            }
        }
        private string _state;
        public string State
        {
            get { return _state; }
            set
            {
                _state = value;
                OnPropertyChanged(nameof(State));
            }
        }
    }
}