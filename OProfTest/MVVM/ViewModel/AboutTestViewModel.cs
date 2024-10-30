using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OProfTest.MVVM.View;
using OProfTest.MVVM.Model;
using OProfTest.Repositories;
using System.Windows.Forms;

namespace OProfTest.MVVM.ViewModel
{
    internal class AboutTestViewModel : ObservableObject
    {
        private readonly QuestionsRepository _questionsRepository;
        private readonly AnswersRepository _answersRepository;

        private User _currentUser;
        private Test _currentTest;

        public AboutTestViewModel(User currentUser, Test currentTest) {
            _questionsRepository = new QuestionsRepository();
            _answersRepository = new AnswersRepository();

            _currentUser = currentUser;
            _currentTest = currentTest;
            Title = _currentTest.Title;
            Description = _currentTest.Description;
            ImageBytes = _currentTest.ImageBytes;
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private byte[] _imageBytes;
        public byte[] ImageBytes
        {
            get { return _imageBytes; }
            set
            {
                _imageBytes = value;
                OnPropertyChanged(nameof(ImageBytes));
            }
        }

        private readonly RelayCommand _startTest;
        public RelayCommand StartTest
        {
            get
            {
                return _startTest ?? (new RelayCommand(obj =>
                {
                    try
                    {
                        var questions = _questionsRepository.GetAllQuestionsByTestID(_currentTest.ID);
                        foreach (var question in questions)
                        {
                            var answers = _answersRepository.GetAllAnswersByTestID(_currentTest.ID);
                            if (answers.Count < 1)
                                throw new Exception("Нет вопросов");
                        }
                        if (_currentTest.ID == 1)
                        {
                            var win = new InterestsTest(_currentUser, _currentTest);
                            App.Current.Windows[0].Close();
                            win.Show();
                        }
                        else if (_currentTest.ID == 2)
                        {
                            var win = new OrientationTest(_currentUser, _currentTest);
                            App.Current.Windows[0].Close();
                            win.Show();
                        }
                        else if (_currentTest.ID == 3)
                        {
                            var win = new InelinationTest(_currentUser, _currentTest);
                            App.Current.Windows[0].Close();
                            win.Show();
                        }
                        else if (_currentTest.ID == 4)
                        {
                            var win = new BrigsTest(_currentUser, _currentTest);
                            App.Current.Windows[0].Close();
                            win.Show();
                        }
                        else if(_currentTest.ID == 5)
                        {
                            var win = new SocionalTypeTest(_currentUser, _currentTest);
                            App.Current.Windows[0].Close();
                            win.Show();
                        }
                        else if (_currentTest.ID == 6)
                        {
                            var win = new ThinkingTypeTest(_currentUser, _currentTest);
                            App.Current.Windows[0].Close();
                            win.Show();
                        }
                        else if (_currentTest.ID == 7)
                        {
                            var win = new ProfTypeTest(_currentUser, _currentTest);
                            App.Current.Windows[0].Close();
                            win.Show();
                        }
                        else
                        {
                            MessageBox.Show("hmmmmmmm...");
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Этот тест ещё не готов к прохождению");
                        var win = new TestsCatalog(_currentUser);
                        App.Current.Windows[0].Close();
                        win.Show();
                    }
                }));
            }
        }
    }
}
