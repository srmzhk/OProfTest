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
    internal class EditTestViewModel : ObservableObject
    {
        private readonly TestsRepository _testsRepository;
        private readonly TestsImagesRepository _testsImagesRepository;

        private Test _selectedTest;

        public ObservableCollection<Test> Tests { get; set; }

        public EditTestViewModel()
        {
            _testsRepository = new TestsRepository();
            _testsImagesRepository = new TestsImagesRepository();
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
                Title = _selectedTest.Title;
                Description = _selectedTest.Description;
                ImagePath = _testsImagesRepository.GetImageByTestId(_selectedTest.ID).FilePath;
                OnPropertyChanged("SelectedTest");
            }
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
        private string _imagePath = "../../Image/add-image.png";
        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                _imagePath = value;
                OnPropertyChanged(nameof(ImagePath));
            }
        }
        private readonly RelayCommand _openFileDialog;
        public RelayCommand OpenFileDialog
        {
            get
            {
                return _openFileDialog ?? (new RelayCommand(obj =>
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Multiselect = false;
                    ofd.Title = "Choose photo";
                    ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.apng;*.avif;*.gif;*.jfif;*.pjpeg";
                    ofd.ShowDialog();
                    ImagePath = ofd.FileName;
                }));
            }
        }
        private readonly RelayCommand _changeTest;
        public RelayCommand ChangeTest
        {
            get
            {
                return _changeTest ?? (new RelayCommand(obj =>
                {
                    try
                    {
                        if (_selectedTest == null)
                            MessageBox.Show("Выберите тест!");
                        else
                        {
                            if (string.IsNullOrEmpty(ImagePath))
                                MessageBox.Show("Выберите картинку!");
                            else if(string.IsNullOrEmpty(Title))
                                MessageBox.Show("Введите название теста!");
                            else if (string.IsNullOrEmpty(Description))
                                MessageBox.Show("Введите описание теста!");
                            else
                            {
                                Test changedTest = new Test();
                                changedTest.ID = _selectedTest.ID;
                                changedTest.Title = Title;
                                changedTest.Description = Description;  
                                _testsRepository.UpdateTest(changedTest);
                                TestImage tm = _testsImagesRepository.ReturnNewTestImage(ImagePath, _selectedTest.ID);
                                _testsImagesRepository.UpdateImageByTestId(_selectedTest.ID, tm);
                                System.Windows.MessageBox.Show("Тест успешно изменён!");
                                App.Current.Windows[0].Close();
                            }
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
