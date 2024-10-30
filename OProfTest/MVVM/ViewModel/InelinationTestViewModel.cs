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
using GroupDocs.Merger;
using Spire.Doc.Fields.Shapes.Charts;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using System.IO;

namespace OProfTest.MVVM.ViewModel
{
    internal class InelinationTestViewModel : ObservableObject
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

        public InelinationTestViewModel(User user, Test test)
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

            LoadQuestions();
            LoadAnswersByTestID(test.ID);

            CompletedQuestionsCount = 0;
            _currentQuestion = Questions[CompletedQuestionsCount];
            TestTitle = _currentTest.Title;
            QuestionTitle = _currentQuestion.Title;
            SubQuestionTitle = "Верно ли, что в детстве Вам очень нравилось:";
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
                            switch (CompletedQuestionsCount)
                            {
                                case 12:
                                    SubQuestionTitle = "Нравится ли Вам сейчас:";
                                    break;
                                case 28:
                                    SubQuestionTitle = "Получаете ли Вы особое удовлетворение:";
                                    break;
                                case 42:
                                    SubQuestionTitle = "Вам нравится, если Вам поручают:";
                                    break;
                                case 56:
                                    SubQuestionTitle = "Всегда ли Вам приятно:";
                                    break;
                                case 70:
                                    SubQuestionTitle = "Можете ли Вы долгое время выдерживать такие дела, как:";
                                    break;
                                case 84:
                                    SubQuestionTitle = "В будущем Вам больше всего хотелось бы:";
                                    break;
                            }

                        }
                        else
                        {
                            foreach (var score in scores)
                            {
                                string key = "";
                                switch (score.Key)
                                {
                                    case 1:
                                        key = "спортивно–физическая";
                                        break;
                                    case 2:
                                        key = "аналитико-математическая";
                                        break;
                                    case 3:
                                        key = "конструкторско–техническая";
                                        break;
                                    case 4:
                                        key = "обращение со знаковыми системами";
                                        break;
                                    case 5:
                                        key = "филологическая";
                                        break;
                                    case 6:
                                        key = "художественная";
                                        break;
                                    case 7:
                                        key = "изобразительная";
                                        break;
                                    case 8:
                                        key = "музыкальная";
                                        break;
                                    case 9:
                                        key = "природоохранная и сельскохозяйственная";
                                        break;
                                    case 10:
                                        key = "коммуникативная";
                                        break;
                                    case 11:
                                        key = "организаторская";
                                        break;
                                    case 12:
                                        key = "социально–педагогическая";
                                        break;
                                    case 13:
                                        key = "социально–педагогическая";
                                        break;
                                    case 14:
                                        key = "социально–педагогическая";
                                        break;
                                    default:
                                        key = "пу пу пу";
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
                        Thread.Sleep(100);
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
            string path = Path.GetFullPath(@"../../../tests/inelination/");

            //слияние трёх соц-подагогоических значений
            Dictionary<int, int> cuttingScores = new Dictionary<int, int>();
            for (int i = 1; i < 12; i++)
            {
                cuttingScores.Add(i, scores[i]);
            }
            cuttingScores.Add(12, scores[12] + scores[13] + scores[14]);

            var sortedScores = cuttingScores.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);

            string filePath1 = Path.GetFullPath(path + sortedScores.ElementAt(0).Key + ".docx");
            string filePath2 = Path.GetFullPath(path + sortedScores.ElementAt(1).Key + ".docx");
            string filePath3 = Path.GetFullPath(path + sortedScores.ElementAt(2).Key + ".docx");

            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
            wordApp.Visible = false;
            Microsoft.Office.Interop.Word.Document sourceDoc1 = wordApp.Documents.Open(filePath1);
            Microsoft.Office.Interop.Word.Document sourceDoc2 = wordApp.Documents.Open(filePath2);
            Microsoft.Office.Interop.Word.Document sourceDoc3 = wordApp.Documents.Open(filePath3);
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

            foreach (Microsoft.Office.Interop.Word.Range range in sourceDoc3.StoryRanges)
            {
                range.Copy();
                endRange.Paste();
            }
            endRange.Collapse(Microsoft.Office.Interop.Word.WdCollapseDirection.wdCollapseEnd);

            if (sortedScores.ElementAt(2).Value == sortedScores.ElementAt(3).Value)
            {
                string filePath4 = Path.GetFullPath(path + sortedScores.ElementAt(3).Key + ".docx");
                Microsoft.Office.Interop.Word.Document sourceDoc4 = wordApp.Documents.Open(filePath4);
                foreach (Microsoft.Office.Interop.Word.Range range in sourceDoc4.StoryRanges)
                {
                    range.Copy();
                    endRange.Paste();
                }
                endRange.Collapse(Microsoft.Office.Interop.Word.WdCollapseDirection.wdCollapseEnd);
                sourceDoc4.Close();
                if (sortedScores.ElementAt(3).Value == sortedScores.ElementAt(4).Value)
                {
                    string filePath5 = Path.GetFullPath(path + sortedScores.ElementAt(4).Key + ".docx");
                    Microsoft.Office.Interop.Word.Document sourceDoc5 = wordApp.Documents.Open(filePath5);
                    foreach (Microsoft.Office.Interop.Word.Range range in sourceDoc5.StoryRanges)
                    {
                        range.Copy();
                        endRange.Paste();
                    }
                    endRange.Collapse(Microsoft.Office.Interop.Word.WdCollapseDirection.wdCollapseEnd);
                    sourceDoc5.Close();
                }
            }

            targetDoc.Save();
            targetDoc.Close();
            // Закрыть источник документа
            sourceDoc1.Close();
            sourceDoc2.Close();
            sourceDoc3.Close();
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
            ShapeObject shape = paragraph.AppendChart(ChartType.Bar, 500, 255);
            paragraph.AppendText("\n\n");

            //Clear the default series of the chart
            Chart chart = shape.Chart;
            chart.Series.Clear();

            string[] categories = new string[12];
            double[] values = new double[12];
            int counter = 0;
            foreach (var score in scores)
            {
                switch (score.Key)
                {
                    case 1:
                        categories[counter] = "спортивно–физическая";
                        break;
                    case 2:
                        categories[counter] = "аналитико-математическая";
                        break;
                    case 3:
                        categories[counter] = "конструкторско–техническая";
                        break;
                    case 4:
                        categories[counter] = "обращение со знаковыми системами";
                        break;
                    case 5:
                        categories[counter] = "филологическая";
                        break;
                    case 6:
                        categories[counter] = "художественная";
                        break;
                    case 7:
                        categories[counter] = "изобразительная";
                        break;
                    case 8:
                        categories[counter] = "музыкальная";
                        break;
                    case 9:
                        categories[counter] = "природоохранная и сельскохозяйственная";
                        break;
                    case 10:
                        categories[counter] = "коммуникативная";
                        break;
                    case 11:
                        categories[counter] = "организаторская";
                        break;
                    case 12:
                        categories[counter] = "социально–педагогическая";
                        break;
                    default:
                        categories[counter] = "пу пу пу";
                        break;
                }
                if(score.Key == 12)
                {
                    values[counter++] = score.Value + scores.ElementAt(12).Value + scores.ElementAt(13).Value;
                    break;
                }
                else
                    values[counter++] = score.Value;
            }

            //Add data series to the chart
            chart.Series.Add("", categories, values);

            //Set chart title
            chart.Title.Text = "Склонности";
            //Set the number format of the Y-axis tick labels to group digits with commas
            chart.AxisY.NumberFormat.FormatCode = "#,##0";

            //Save the result document
            string path = Path.GetFullPath(@"../../../results/" + _currentUser.LastName + "-Склонности-" + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + ".docx");
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
        private string _subQuestionTitle;
        public string SubQuestionTitle
        {
            get { return _subQuestionTitle; }
            set
            {
                _subQuestionTitle = value;
                OnPropertyChanged(nameof(SubQuestionTitle));
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