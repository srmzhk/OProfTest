using OProfTest.MVVM.Model;
using OProfTest.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OProfTest.MVVM.ViewModel
{
    internal class DeleteTestViewModel : ObservableObject
    {
        private readonly TestsRepository _testsRepository;
        private readonly TestsImagesRepository _testsImagesRepository;
        private readonly QuestionsRepository _questionsRepository;
        private readonly AnswersRepository _answersRepository;
        private readonly ResultsRepository _resultsRepository;

        private Test _selectedTest;

        public ObservableCollection<Test> Tests { get; set; }

        public DeleteTestViewModel()
        {
            _testsRepository = new TestsRepository();
            _testsImagesRepository = new TestsImagesRepository();
            _questionsRepository = new QuestionsRepository();
            _answersRepository = new AnswersRepository();
            _resultsRepository = new ResultsRepository();
            Tests = new ObservableCollection<Test>();
            LoadTests();
        }

        private void LoadTests()
        {
            var tests = _testsRepository.GetAllTests();
            Tests.Clear();
            foreach (var test in tests)
                Tests.Add(test);
        }

        public Test SelectedTest
        {
            get => _selectedTest;
            set
            {
                _selectedTest = value;
                OnPropertyChanged("SelectedTest");
            }
        }

        private readonly RelayCommand _deleteTest;
        public RelayCommand DeleteTest
        {
            get
            {
                return _deleteTest ?? (new RelayCommand(obj =>
                {
                    try
                    {
                        if (_selectedTest == null)
                            MessageBox.Show("Выберите тест!");
                        else
                        {
                            var questions = _questionsRepository.GetAllQuestionsByTestID(_selectedTest.ID);
                            foreach (var question in questions)
                            {
                                _answersRepository.DeleteAnswersByTestID(_selectedTest.ID);
                            }
                            _resultsRepository.DeleteResultsByTestID(_selectedTest.ID);
                            _questionsRepository.DeleteQuestionsByTestID(_selectedTest.ID);
                            _testsImagesRepository.DeleteImageByTestId(_selectedTest.ID);
                            _testsRepository.DeleteTestById(_selectedTest.ID);
                            MessageBox.Show("Удаление со всем связующими выполнено успешно!");
                            App.Current.Windows[0].Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show("Произошла ошибка!" + ex.Message);
                    }
                }));
            }
        }
    }
}
