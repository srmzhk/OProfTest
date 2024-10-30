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
using System.IO;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIO;
using Syncfusion.OfficeChart;

namespace OProfTest.MVVM.ViewModel
{
    internal class OrientationTestViewModel : ObservableObject
    {
        private User _currentUser;
        private Test _currentTest;
        private Answer _selectedAnswer;
        private Question _currentQuestion;

        private readonly QuestionsRepository _questionsRepository;
        private readonly AnswersRepository _answersRepository;

        public ObservableCollection<Question> Questions { get; set; }
        public ObservableCollection<Answer> Answers { get; set; }

        private Dictionary<int, int> scoresWant;
        private Dictionary<int, int> scoresCan;
        private string strToWrite;

        public OrientationTestViewModel(User user, Test test)
        {
            _questionsRepository = new QuestionsRepository();
            _answersRepository = new AnswersRepository();

            Questions = new ObservableCollection<Question>();
            Answers = new ObservableCollection<Answer>();

            _currentUser = user;
            _currentTest = test;
            _selectedAnswer = new Answer();

            scoresWant = new Dictionary<int, int>();
            scoresCan = new Dictionary<int, int>();
            strToWrite = "";

            LoadQuestions();
            LoadAnswersByTestID(test.ID);

            CompletedQuestionsCount = 0;
            _currentQuestion = Questions[CompletedQuestionsCount];
            TestTitle = _currentTest.Title;
            QuestionTitle = _currentQuestion.Title;
            SubQuestionTitle = "Я хочу (мне нравится, меня привлекает, я предпочитаю):";
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
                        if(CompletedQuestionsCount < 35)
                        {
                            if (scoresWant.ContainsKey(_currentQuestion.QuestionType))
                            {
                                if (_selectedAnswer.Value > 0)
                                    scoresWant[_currentQuestion.QuestionType] += _selectedAnswer.Value;
                            }
                            else
                            {
                                scoresWant.Add(_currentQuestion.QuestionType, 0);
                                if (_selectedAnswer.Value > 0)
                                    scoresWant[_currentQuestion.QuestionType] += _selectedAnswer.Value;
                            }
                        }
                        else
                        {
                            if (scoresCan.ContainsKey(_currentQuestion.QuestionType))
                            {
                                if (_selectedAnswer.Value > 0)
                                    scoresCan[_currentQuestion.QuestionType] += _selectedAnswer.Value;
                            }
                            else
                            {
                                scoresCan.Add(_currentQuestion.QuestionType, 0);
                                if (_selectedAnswer.Value > 0)
                                    scoresCan[_currentQuestion.QuestionType] += _selectedAnswer.Value;
                            }
                        }

                        CompletedQuestionsCount++;
                        if (CompletedQuestionsCount != _amountOfQuestions)
                        {
                            _currentQuestion = Questions[CompletedQuestionsCount];
                            QuestionTitle = _currentQuestion.Title;
                            if (CompletedQuestionsCount == 35)
                                SubQuestionTitle = "Я могу (способен, умею, обладаю навыками)";
                        }
                        else
                        {
                            foreach (var score in scoresWant)
                            {
                                string key = "";
                                switch (score.Key)
                                {
                                    case 1:
                                        key = "человек-человек";
                                        break;
                                    case 2:
                                        key = "человек-техника";
                                        break;
                                    case 3:
                                        key = "человек-информация ";
                                        break;
                                    case 4:
                                        key = "человек–искусство";
                                        break;
                                    case 5:
                                        key = "человек-природа";
                                        break;
                                    case 6:
                                        key = "А";
                                        break;
                                    case 7:
                                        key = "Б";
                                        break;
                                    default:
                                        key = "пу пу пу";
                                        break;
                                }
                                strToWrite += key + ": " + score.Value + "\n";
                            }

                            string path = CreateChart();
                            ClearDoc(path);
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
            string path = Path.GetFullPath(@"../../../tests/orientation/");

            int counter = 1;
            Dictionary<int, int> mergeDict = new Dictionary<int, int>();

            foreach (var item in scoresWant) { 
                mergeDict.Add(item.Key, item.Value + scoresCan[counter]);
                counter++;
            }

            var sortedMergeDict = mergeDict.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);

            //two main skills

            string filePath1 = Path.GetFullPath(path + mergeDict.ElementAt(0).Key + ".docx");
            string filePath2 = Path.GetFullPath(path + mergeDict.ElementAt(1).Key + ".docx");
            //A or B
            string filePath3 = "";
            if (mergeDict.ElementAt(5).Value > mergeDict.ElementAt(6).Value)
                filePath3 = Path.GetFullPath(path + mergeDict.ElementAt(5).Key + ".docx");
            else
                filePath3 = Path.GetFullPath(path + mergeDict.ElementAt(6).Key + ".docx");

            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
            wordApp.Visible = false;
            Microsoft.Office.Interop.Word.Document targetDoc = wordApp.Documents.Open(targetPath);
            Microsoft.Office.Interop.Word.Document sourceDoc1 = wordApp.Documents.Open(filePath1);
            Microsoft.Office.Interop.Word.Document sourceDoc2 = wordApp.Documents.Open(filePath2);
            Microsoft.Office.Interop.Word.Document sourceDoc3 = wordApp.Documents.Open(filePath3);
            Microsoft.Office.Interop.Word.Range endRange = targetDoc.Content;
            endRange.Collapse(Microsoft.Office.Interop.Word.WdCollapseDirection.wdCollapseEnd);

            //изменение полей страницы
            var pageSetup = targetDoc.PageSetup;
            float cmToPoints = 28.35f; // Коэффициент для преобразования сантиметров в пункты (точки)
            pageSetup.LeftMargin = 1.5f * cmToPoints; // Левое поле (2.5 см)
            pageSetup.RightMargin = 1f * cmToPoints; // Правое поле (2.5 см)
            pageSetup.TopMargin = 1f * cmToPoints; // Верхнее поле (2.0 см)
            pageSetup.BottomMargin = 1f * cmToPoints; // Нижнее поле (2.0 см)

            // Copy the content from the source document to the target document
            foreach (Microsoft.Office.Interop.Word.Range range in sourceDoc1.StoryRanges)
            {
                range.Copy();
                endRange.Paste();
            }
            endRange.Collapse(Microsoft.Office.Interop.Word.WdCollapseDirection.wdCollapseEnd);
            endRange.InsertAfter("\n\n");
            endRange.Collapse(Microsoft.Office.Interop.Word.WdCollapseDirection.wdCollapseEnd);

            foreach (Microsoft.Office.Interop.Word.Range range in sourceDoc2.StoryRanges)
            {
                range.Copy();
                endRange.Paste();
            }
            endRange.Collapse(Microsoft.Office.Interop.Word.WdCollapseDirection.wdCollapseEnd);
            endRange.InsertAfter("\n\n");
            endRange.Collapse(Microsoft.Office.Interop.Word.WdCollapseDirection.wdCollapseEnd);

            foreach (Microsoft.Office.Interop.Word.Range range in sourceDoc3.StoryRanges)
            {
                range.Copy();
                endRange.Paste();
            }

            // Закрыть и сохранить исходный документ
            targetDoc.Save();
            targetDoc.Close();
            // Закрыть источник документа
            sourceDoc1.Close();
            sourceDoc2.Close();
            sourceDoc3.Close();

            // Закрыть приложение Word
            wordApp.Quit();
        }

        private void ClearDoc(string path)
        {
            Spire.Doc.Document document = new Spire.Doc.Document();
            document.LoadFromFile(path);

            // Удаляем колонтитулы из всех секций документа
            foreach (Spire.Doc.Section section in document.Sections)
            {
                // Очищаем содержимое верхнего колонтитула
                if (section.HeadersFooters.Header != null)
                    section.HeadersFooters.Header.ChildObjects.Clear();

                // Очищаем содержимое нижнего колонтитула
                if (section.HeadersFooters.Footer != null)
                    section.HeadersFooters.Footer.ChildObjects.Clear();
            }

            if (document.Sections.Count > 0 && document.Sections[0].Paragraphs.Count > 0)
            {
                document.Sections[0].Paragraphs.RemoveAt(0);
            }

            // Удаляем последний параграф
            if (document.Sections.Count > 0 && document.Sections[document.Sections.Count - 1].Paragraphs.Count > 0)
            {
                document.Sections[document.Sections.Count - 1].Paragraphs.RemoveAt(document.Sections[document.Sections.Count - 1].Paragraphs.Count - 1);
            }

            // Сохраняем изменения
            document.SaveToFile(path, FileFormat.Docx);
        }

        private string CreateChart()
        {
            string path = "";
            using (WordDocument document = new WordDocument())
            {
                // Add a section to the document.
                IWSection section = document.AddSection();

                //user data
                IWParagraph paragraphText = section.AddParagraph();
                paragraphText.AppendText(_currentUser.LastName + " " + _currentUser.FirstName + "\n");

                //custom style for user data paragraph
                var myStyle = document.AddParagraphStyle("MyCustomStyle");
                myStyle.CharacterFormat.FontName = "Times New Roman";
                myStyle.CharacterFormat.FontSize = 14;
                myStyle.CharacterFormat.Bold = true;
                paragraphText.ApplyStyle("MyCustomStyle");

                //Add a paragraph to the section.
                IWParagraph paragraph = section.AddParagraph();

                //Create and append the chart to the paragraph.
                WChart chart = paragraph.AppendChart(520, 216);

                paragraph.AppendText("\n\n\n");

                //убираем линии на заднем фоне
                chart.PrimaryValueAxis.HasMajorGridLines = false;
                chart.PrimaryValueAxis.HasMinorGridLines = false;

                //размер подписи оси Ox
                var categoryAxis = chart.PrimaryCategoryAxis;
                categoryAxis.Font.Size = 8;

                //Set chart type.
                chart.ChartType = OfficeChartType.Column_Clustered;
                chart.ChartArea.Fill.FillType = OfficeFillType.SolidColor;
                //Assign data.
                chart.ChartData.SetValue(1, 2, "Хочу");
                chart.ChartData.SetValue(1, 3, "Могу");

                string[] categories = new string[7];

                int k = 0;
                foreach (var score in scoresWant)
                {
                    switch (score.Key)
                    {
                        case 1:
                            categories[k] = "Человек-\nчеловек";
                            break;
                        case 2:
                            categories[k] = "Человек-\nтехника";
                            break;
                        case 3:
                            categories[k] = "Человек-\nинформация ";
                            break;
                        case 4:
                            categories[k] = "Человек-\nискусство";
                            break;
                        case 5:
                            categories[k] = "Человек-\nприрода";
                            break;
                        case 6:
                            categories[k] = "Исполнительский\nхарактер труда";
                            break;
                        case 7:
                            categories[k] = "Творческий\nхарактер\nтруда";
                            break;
                        default:
                            categories[k] = "пу пу пу";
                            break;
                    }
                    k++;
                }

                int counter = 0;
                for (int i = 2; i < 9; i++)
                {
                    chart.ChartData.SetValue(i, 1, categories[counter++]);
                    chart.ChartData.SetValue(i, 2, scoresWant[counter]);
                    chart.ChartData.SetValue(i, 3, scoresCan[counter]);
                }

                //Set chart series in the column for assigned data region.
                chart.IsSeriesInRows = false;
                //Set a Chart Title.
                chart.ChartTitle = "Ориентация";
                chart.ChartTitleArea.Size = 14;
                //Set Datalabels.
                IOfficeChartSerie series1 = chart.Series.Add("Хочу");
                //Set the data range of chart series – start row, start column, end row, and end column.
                series1.Values = chart.ChartData[2, 2, 8, 2];
                IOfficeChartSerie series2 = chart.Series.Add("Могу");
                //Set the data range of chart series start row, start column, end row, and end column.
                series2.Values = chart.ChartData[2, 3, 8, 3];
                //Set the data range of the category axis.
                chart.PrimaryCategoryAxis.CategoryLabels = chart.ChartData[2, 1, 8, 1];

                //подписи сверху колонок
                series1.DataPoints.DefaultDataPoint.DataLabels.IsValue = true;
                series2.DataPoints.DefaultDataPoint.DataLabels.IsValue = true;
                series1.DataPoints.DefaultDataPoint.DataLabels.Position = OfficeDataLabelPosition.Outside;
                series2.DataPoints.DefaultDataPoint.DataLabels.Position = OfficeDataLabelPosition.Outside;

                //размер подписей
                series1.DataPoints.DefaultDataPoint.DataLabels.Size = 9;
                series2.DataPoints.DefaultDataPoint.DataLabels.Size = 9;

                //границы
                series1.SerieFormat.LineProperties.LineColor = Syncfusion.Drawing.Color.Black;
                series1.SerieFormat.LineProperties.LinePattern = OfficeChartLinePattern.Solid;
                series1.SerieFormat.LineProperties.LineWeight = OfficeChartLineWeight.Medium;

                series2.SerieFormat.LineProperties.LineColor = Syncfusion.Drawing.Color.Black;
                series2.SerieFormat.LineProperties.LinePattern = OfficeChartLinePattern.Solid;
                series2.SerieFormat.LineProperties.LineWeight = OfficeChartLineWeight.Medium;

                // Устанавливаем тип штриховки
                series1.SerieFormat.Fill.Pattern = OfficeGradientPattern.Pat_5_Percent;
                series1.SerieFormat.Fill.BackColor = Syncfusion.Drawing.Color.White;
                series1.SerieFormat.Fill.ForeColor = Syncfusion.Drawing.Color.Black;

                series2.SerieFormat.Fill.Pattern = OfficeGradientPattern.Pat_Wide_Upward_Diagonal;
                series2.SerieFormat.Fill.BackColor = Syncfusion.Drawing.Color.White;
                series2.SerieFormat.Fill.ForeColor = Syncfusion.Drawing.Color.Black;

                //Set legend.
                chart.HasLegend = true;
                chart.Legend.Position = OfficeLegendPosition.Bottom;

                //Create a file stream.
                path = Path.GetFullPath(@"../../../results/" + _currentUser.LastName + "-Ориентация-" + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + ".docx");
                using (FileStream outputFileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                {
                    //Save the Word document to the file stream.
                    document.Save(outputFileStream, FormatType.Docx);
                }
            }
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