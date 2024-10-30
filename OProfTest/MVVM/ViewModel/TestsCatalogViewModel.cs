using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OProfTest.MVVM.Model;
using OProfTest.MVVM.View;
using OProfTest.Repositories;

namespace OProfTest.MVVM.ViewModel
{
    internal class TestsCatalogViewModel : ObservableObject
    {
        private User _currentUser;
        private readonly TestsRepository _testsRepository;
        private readonly TestsImagesRepository _testsImagesRepository;

        public ObservableCollection<Test> Tests { get; set; }

        private Test _selectedTest;

        public TestsCatalogViewModel(User currentUser)
        {
            _testsRepository = new TestsRepository();
            _testsImagesRepository = new TestsImagesRepository();
            Tests = new ObservableCollection<Test>();
            _selectedTest = new Test();
            _currentUser = currentUser;
            LoadTests();
        }

        public Test SelectedTest
        {
            get => _selectedTest;
            set
            {
                _selectedTest = value;                
                OnPropertyChanged("SelectedTest");
                if (value != null)
                {
                    if (App.Current.Windows.Count > 1)
                        App.Current.Windows[1].Close();
                    var win = new AboutTest(_currentUser, _selectedTest);
                    App.Current.Windows[0].Close();
                    win.Show();
                }
            }
        }

        private void LoadTests()
        {
            Tests.Clear();
            var tests = _testsRepository.GetAllTests();
            foreach (var test in tests)
            {
                Console.WriteLine($"Test Title: {test.Title}");
                Tests.Add(test);
                test.ImageBytes = _testsImagesRepository.GetImageByTestId(test.ID).Image;
            }
        }
    }
}
