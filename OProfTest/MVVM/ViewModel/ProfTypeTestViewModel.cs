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
using Spire.Doc.Fields.Shapes.Charts;
using Spire.Doc;
using Spire.Doc.Fields;
using Spire.Doc.Documents;

namespace OProfTest.MVVM.ViewModel
{
    internal class ProfTypeTestViewModel : ObservableObject
    {
        private User _currentUser;
        private Test _currentTest;
        private Answer _selectedAnswer;

        private readonly AnswersRepository _answersRepository;

        public ObservableCollection<Question> Questions { get; set; }
        public ObservableCollection<Answer> Answers { get; set; }

        private Dictionary<string, int> scores;
        private string strToWrite;

        public ProfTypeTestViewModel(User user, Test test)
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
            AmountOfQuestions = 42;
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
            string path = Path.GetFullPath(@"../../../tests/profType/");

            var sortedScores = scores.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);

            string filePath1 = Path.GetFullPath(path + sortedScores.ElementAt(0).Key + ".docx");
            string filePath2 = Path.GetFullPath(path + sortedScores.ElementAt(1).Key + ".docx");

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

            // Copy the content from the source document to the target document
            foreach (Microsoft.Office.Interop.Word.Range range in sourceDoc2.StoryRanges)
            {
                range.Copy();
                endRange.Paste();
            }
            endRange.Collapse(Microsoft.Office.Interop.Word.WdCollapseDirection.wdCollapseEnd);

            if (sortedScores.ElementAt(1).Value == sortedScores.ElementAt(2).Value)
            {
                string filePath3 = Path.GetFullPath(path + sortedScores.ElementAt(2).Key + ".docx");
                Microsoft.Office.Interop.Word.Document sourceDoc3 = wordApp.Documents.Open(filePath3);
                foreach (Microsoft.Office.Interop.Word.Range range in sourceDoc3.StoryRanges)
                {
                    range.Copy();
                    endRange.Paste();
                }
                sourceDoc3.Close();
            }

            targetDoc.Save();
            targetDoc.Close();
            // Закрыть источник документа
            sourceDoc1.Close();
            sourceDoc2.Close();
            // Закрыть приложение Word
            wordApp.Quit();
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
            ShapeObject shape = paragraph.AppendChart(ChartType.Bar, 350, 200);
            paragraph.AppendText("\n\n");

            //Clear the default series of the chart
            Chart chart = shape.Chart;
            chart.Series.Clear();

            var sortedScores = scores.OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
            string[] categories = new string[6];
            double[] values = new double[6];

            int counter = 0;
            foreach (var item in sortedScores)
            {
                categories[counter] = item.Key;
                values[counter++] = item.Value;
            }

            //Add data series to the chart
            chart.Series.Add("", categories, values);

            //Set chart title
            chart.Title.Text = "Профессиональный тип";
            //Set the number format of the Y-axis tick labels to group digits with commas
            chart.AxisY.NumberFormat.FormatCode = "#,##0";

            //Save the result document
            string path = Path.GetFullPath(@"../../../results/" + _currentUser.LastName + "-Проф.тип-" + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + ".docx");
            document.SaveToFile(path, FileFormat.Docx2016);
            document.Dispose();
            return path;
        }

        private void InsertValues()
        {
            scores.Add("A", 0);
            scores.Add("C", 0);
            scores.Add("I", 0);
            scores.Add("E", 0);
            scores.Add("R", 0);
            scores.Add("S", 0);
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