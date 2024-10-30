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
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Doc.Fields.Shapes.Charts;

namespace OProfTest.MVVM.ViewModel
{
    internal class ThinkingTypeTestViewModel : ObservableObject
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

        public ThinkingTypeTestViewModel(User user, Test test)
        {
            _questionsRepository = new QuestionsRepository();
            _answersRepository = new AnswersRepository();

            Questions = new ObservableCollection<Question>();
            Answers = new ObservableCollection<Answer>();

            _currentUser = user;
            _currentTest = test;
            _selectedAnswer = new Answer();

            scores = new Dictionary<int, int>();
            strToWrite = "";

            InsertValues();
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
                                        key = "Предметно-действенное";
                                        break;
                                    case 2:
                                        key = "Абстрактно символическое";
                                        break;
                                    case 3:
                                        key = "Словесно-логическое";
                                        break;
                                    case 4:
                                        key = "Наглядно-образное";
                                        break;
                                    case 5:
                                        key = "Креативность";
                                        break;
                                    default:
                                        break;
                                }
                                strToWrite += key + ": " + score.Value + "\n";
                            }

                            string path = CreateChart();
                            MergeDocs(path); 
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

        private void MergeDocs(string targetPath)
        {
            string path = Path.GetFullPath(@"../../../tests/thinkingType/");

            var sortedScores = scores.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);

            string filePath1 = Path.GetFullPath(path + sortedScores.ElementAt(0).Key.ToString() + ".docx");
            string filePath2 = Path.GetFullPath(path + sortedScores.ElementAt(1).Key.ToString() + ".docx");

            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
            wordApp.Visible = false;
            Microsoft.Office.Interop.Word.Document sourceDoc1 = wordApp.Documents.Open(filePath1);
            Microsoft.Office.Interop.Word.Document sourceDoc2 = wordApp.Documents.Open(filePath2);
            Microsoft.Office.Interop.Word.Document targetDoc = wordApp.Documents.Open(targetPath);
            Microsoft.Office.Interop.Word.Range endRange = targetDoc.Content;

            //изменение полей страницы
            var pageSetup = targetDoc.PageSetup;
            float cmToPoints = 28.35f; // Коэффициент для преобразования сантиметров в пункты (точки)
            pageSetup.LeftMargin = 1.5f * cmToPoints; // Левое поле (2.5 см)
            pageSetup.RightMargin = 1f * cmToPoints; // Правое поле (2.5 см)
            pageSetup.TopMargin = 1f * cmToPoints; // Верхнее поле (2.0 см)
            pageSetup.BottomMargin = 1f * cmToPoints; // Нижнее поле (2.0 см)

            endRange.Collapse(Microsoft.Office.Interop.Word.WdCollapseDirection.wdCollapseEnd);

            // Copy the content from the source document to the target document
            foreach (Microsoft.Office.Interop.Word.Range range in sourceDoc1.StoryRanges)
            {
                range.Copy();
                endRange.Paste();
            }
            endRange.Collapse(Microsoft.Office.Interop.Word.WdCollapseDirection.wdCollapseEnd);

            foreach (Microsoft.Office.Interop.Word.Range range in sourceDoc2.StoryRanges)
            {
                range.Copy();
                endRange.Paste();
            }
            endRange.Collapse(Microsoft.Office.Interop.Word.WdCollapseDirection.wdCollapseEnd);

            if (sortedScores.ElementAt(1).Value == sortedScores.ElementAt(2).Value)
            {
                string filePath3 = Path.GetFullPath(path + sortedScores.ElementAt(2).Key.ToString() + ".docx");
                Microsoft.Office.Interop.Word.Document sourceDoc3 = wordApp.Documents.Open(filePath3);
                foreach (Microsoft.Office.Interop.Word.Range range in sourceDoc3.StoryRanges)
                {
                    range.Copy();
                    endRange.Paste();
                }
                sourceDoc3.Close();
            }

            // Закрыть и сохранить исходный документ
            targetDoc.Save();
            targetDoc.Close();
            // Закрыть источник документа
            sourceDoc1.Close();
            sourceDoc2.Close();
            // Закрыть приложение Word
            wordApp.Quit();
        }

        private void InsertValues()
        {
            scores.Add(1, 0);
            scores.Add(2, 0);
            scores.Add(3, 0);
            scores.Add(4, 0);
            scores.Add(5, 0);
        }

        private string CreateChart()
        {
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
            ShapeObject shape = paragraph.AppendChart(ChartType.Bar, 350, 255);
            paragraph.AppendText("\n\n");

            //Clear the default series of the chart
            Chart chart = shape.Chart;
            chart.Series.Clear();

            string[] categories = new string[5];
            double[] values = new double[5];

            var sortedScores = scores.OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);

            int counter = 0;
            foreach (var score in sortedScores)
            {
                switch (score.Key)
                {
                    case 1:
                        categories[counter] = "Предметно-действенное";
                        break;
                    case 2:
                        categories[counter] = "Абстрактно-символическое";
                        break;
                    case 3:
                        categories[counter] = "Словесно-логическое";
                        break;
                    case 4:
                        categories[counter] = "Наглядно-образное";
                        break;
                    case 5:
                        categories[counter] = "Креативность";
                        break;
                    default:
                        break;
                }
                values[counter++] = score.Value;
            }

            //Add data series to the chart
            chart.Series.Add("", categories, values);

            //Set chart title
            chart.Title.Text = "Тип мышления";
            //Set the number format of the Y-axis tick labels to group digits with commas
            chart.AxisY.NumberFormat.FormatCode = "#,##0";

            //Save the result document
            string path = Path.GetFullPath("..\\..\\..\\results\\" + _currentUser.LastName + "-Мышление-" + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + ".docx");
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