using System;
using System.Collections.Generic;
using System.Linq;
using OProfTest.MVVM.Model;
using OProfTest.MVVM.View;
using System.Windows.Forms;
using OProfTest.Repositories;
using System.Collections.ObjectModel;
using System.Threading;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Doc.Fields.Shapes.Charts;
using System.IO;

namespace OProfTest.MVVM.ViewModel
{
    internal class InterestsTestViewModel : ObservableObject
    {
        private User _currentUser;
        private Test _currentTest;
        private Answer _selectedAnswer;
        private Question _currentQuestion;

        private readonly QuestionsRepository _questionsRepository;
        private readonly AnswersRepository _answersRepository;

        public ObservableCollection<Question> Questions { get; set; }
        public ObservableCollection<Answer> Answers { get; set; }

        private Dictionary<int, int> scores;
        private string strToWrite;

        public InterestsTestViewModel(User user, Test test)
        {
            _questionsRepository = new QuestionsRepository();
            _answersRepository = new AnswersRepository();

            Questions = new ObservableCollection<Question>();
            Answers = new ObservableCollection<Answer>();

            _currentUser = user;
            _currentTest = test;
            _selectedAnswer = new Answer();

            scores = new Dictionary<int, int>();
            string strToWrite = "";

            LoadQuestions();
            LoadAnswersByTestID(test.ID);

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
                        if (scores.ContainsKey(_currentQuestion.QuestionType))
                        {
                            if(_selectedAnswer.Value > 0)
                                scores[_currentQuestion.QuestionType] += _selectedAnswer.Value;
                        }
                        else
                        {
                            scores.Add(_currentQuestion.QuestionType, 0);
                            if (_selectedAnswer.Value > 0)
                                scores[_currentQuestion.QuestionType] += _selectedAnswer.Value;
                        }

                        CompletedQuestionsCount++;
                        if (CompletedQuestionsCount != _amountOfQuestions)
                        {
                            _currentQuestion = Questions[CompletedQuestionsCount];
                            QuestionTitle = _currentQuestion.Title;
                        }
                        else
                        {
                            foreach (var score in scores)
                            {
                                string key = "";
                                switch (score.Key)
                                {
                                    case 1:
                                        key = "физика";
                                        break;
                                    case 2:
                                        key = "математика";
                                        break;
                                    case 3:
                                        key = "экономика и бизнес";
                                        break;
                                    case 4:
                                        key = "техника и электротехника";
                                        break;
                                    case 5:
                                        key = "химия";
                                        break;
                                    case 6:
                                        key = "биология и сельское хозяйство";
                                        break;
                                    case 7:
                                        key = "медицина";
                                        break;
                                    case 8:
                                        key = "география и геология";
                                        break;
                                    case 9:
                                        key = "история";
                                        break;
                                    case 10:
                                        key = "филология, журналистика";
                                        break;
                                    case 11:
                                        key = "искусство";
                                        break;
                                    case 12:
                                        key = "педагогика";
                                        break;
                                    case 13:
                                        key = "труд в сфере обслуживания";
                                        break;
                                    case 14:
                                        key = "военное дело";
                                        break;
                                    case 15:
                                        key = "спорт";
                                        break;
                                    default:
                                        key = "???";
                                        break;
                                }
                                strToWrite += key + ": " + score.Value + "\n";
                            }

                            string path = CreateChart();

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

        private string CreateChart(){
            //Create a word document
            Document document = new Document();

            //Create a new section
            Section section = document.AddSection();
            section.PageSetup.Margins.All = 70f;

            //Create a new paragraph and append text
            Paragraph paragraph = section.AddParagraph();
            paragraph.AppendText(_currentUser.FirstName + " " + _currentUser.LastName + "\n");
            paragraph.ApplyStyle(BuiltinStyle.Heading3);

            //Create a new paragraph to append a bar chart shape
            paragraph = section.AddParagraph();
            ShapeObject shape = paragraph.AppendChart(ChartType.Bar, 500, 300);

            //Clear the default series of the chart
            Chart chart = shape.Chart;
            chart.Series.Clear();

            var sortedScores = scores.OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
            string[] categories = new string[15];
            double[] values = new double[15];
            //Specify chart data
            int counter = 0;
            foreach(var score in sortedScores)
            {
                switch (score.Key)
                {
                    case 1:
                        categories[counter] = "физика";
                        break;
                    case 2:
                        categories[counter] = "математика";
                        break;
                    case 3:
                        categories[counter] = "экономика и бизнес";
                        break;
                    case 4:
                        categories[counter] = "техника и электротехника";
                        break;
                    case 5:
                        categories[counter] = "химия";
                        break;
                    case 6:
                        categories[counter] = "биология и сельское хозяйство";
                        break;
                    case 7:
                        categories[counter] = "медицина";
                        break;
                    case 8:
                        categories[counter] = "география и геология";
                        break;
                    case 9:
                        categories[counter] = "история";
                        break;
                    case 10:
                        categories[counter] = "филология, журналистика";
                        break;
                    case 11:
                        categories[counter] = "искусство";
                        break;
                    case 12:
                        categories[counter] = "педагогика";
                        break;
                    case 13:
                        categories[counter] = "труд в сфере обслуживания";
                        break;
                    case 14:
                        categories[counter] = "военное дело";
                        break;
                    case 15:
                        categories[counter] = "спорт";
                        break;
                    default:
                        break;
                }
                values[counter++] = score.Value;
            }

            //Add data series to the chart
            chart.Series.Add("", categories, values);

            //Set chart title
            chart.Title.Text = "Интересы";
            //Set the number format of the Y-axis tick labels to group digits with commas
            chart.AxisY.NumberFormat.FormatCode = "#,##0";

            //Save the result document
            string path = Path.GetFullPath("..\\..\\..\\results\\" + _currentUser.LastName + "-" + _currentTest.Title + "-" + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + ".docx");
            document.SaveToFile(path, FileFormat.Docx2016);
            document.Dispose();
            return path;
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
        private void LoadAnswersByTestID(int id)
        {
            Answers.Clear();
            var answers = _answersRepository.GetAllAnswersByTestID(id);
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