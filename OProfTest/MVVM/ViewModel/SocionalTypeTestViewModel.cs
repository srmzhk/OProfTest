using System;
using System.Collections.Generic;
using System.Linq;
using OProfTest.MVVM.Model;
using OProfTest.MVVM.View;
using System.Windows.Forms;
using OProfTest.Repositories;
using System.Collections.ObjectModel;
using System.Threading;
using System.IO;
using Microsoft.Office.Interop.Word;

namespace OProfTest.MVVM.ViewModel
{
    internal class SocionalTypeTestViewModel : ObservableObject
    {
        private User _currentUser;
        private Test _currentTest;
        private Answer _selectedAnswer;

        private readonly AnswersRepository _answersRepository;

        public ObservableCollection<Question> Questions { get; set; }
        public ObservableCollection<Answer> Answers { get; set; }

        private Dictionary<string, int> scores;
        private string strToWrite;

        public SocionalTypeTestViewModel(User user, Test test)
        {
            _answersRepository = new AnswersRepository();

            Answers = new ObservableCollection<Answer>();

            _currentUser = user;
            _currentTest = test;
            _selectedAnswer = new Answer();

            scores = new Dictionary<string, int>();
            strToWrite = "";

            InsertValues();
            LoadAnswersByType(1);

            CompletedQuestionsCount = 0;
            TestTitle = _currentTest.Title;
            QuestionTitle = "Выберете наиболее верное для вас утверждение:";
            AmountOfQuestions = 20;
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
                            MessageBox.Show("ой-ёй");
                        }

                        CompletedQuestionsCount++;
                        if (CompletedQuestionsCount != _amountOfQuestions)
                        {
                            LoadAnswersByType(_completedQuestionsCount + 1);
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
            string path = Path.GetFullPath(@"../../../tests/socionalType/");
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

            //изменение полей страницы
            var pageSetup = targetDoc.PageSetup;
            float cmToPoints = 28.35f; // Коэффициент для преобразования сантиметров в пункты (точки)
            pageSetup.LeftMargin = 1.5f * cmToPoints; // Левое поле (2.5 см)
            pageSetup.RightMargin = 1f * cmToPoints; // Правое поле (2.5 см)
            pageSetup.TopMargin = 1f * cmToPoints; // Верхнее поле (2.0 см)
            pageSetup.BottomMargin = 1f * cmToPoints; // Нижнее поле (2.0 см)

            endRange.Collapse(Microsoft.Office.Interop.Word.WdCollapseDirection.wdCollapseEnd);

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
            string targetPath = Path.GetFullPath(@"../../../results/" + _currentUser.LastName + "-Соц.тип-" + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + ".docx");
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
            scores.Add("1", 0);
            scores.Add("2", 0);
            scores.Add("3", 0);
            scores.Add("4", 0);
            scores.Add("5", 0);
            scores.Add("6", 0);
            scores.Add("7", 0);
            scores.Add("8", 0);
        }

        private void LoadAnswersByType(int type)
        {
            Answers.Clear();
            var answers = _answersRepository.GetAllAnswersByType(type, _currentTest.ID);
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