using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OProfTest.MVVM.Model;
using System.Windows.Forms;
using System.Windows;
using OProfTest.Repositories;
using System.Xml.Linq;
using OProfTest.MVVM.View;

namespace OProfTest.MVVM.ViewModel
{
    internal class AddNewTestViewModel : ObservableObject
    {
        private readonly TestsRepository _testsRepository;
        private readonly TestsImagesRepository _testsImagesRepository;

        private Test _test;

        public AddNewTestViewModel() {
            _testsRepository = new TestsRepository();
            _testsImagesRepository = new TestsImagesRepository();
            _test = new Test();
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
        private readonly RelayCommand _addTest;
        public RelayCommand AddTest
        {
            get
            {
                return _addTest ?? (new RelayCommand(obj =>
                {
                    try
                    {
                        if (string.IsNullOrEmpty(Title))
                            System.Windows.MessageBox.Show("Заполните поле название!");
                        else if(string.IsNullOrEmpty(Description))
                            System.Windows.MessageBox.Show("Заполните поле Описание!");
                        else
                        {
                            _test.Title = Title;
                            _test.Description = Description;
                            _testsRepository.AddNewTest(_test);
                            Test addedTest = _testsRepository.GetTestByTitle(_test.Title);
                            _testsImagesRepository.AddImageByTestId(ImagePath, addedTest.ID);
                            System.Windows.MessageBox.Show("Тест успешно добавлен!");
                            App.Current.Windows[0].Close();
                        }
                    }
                    catch(Exception ex)
                    {
                        System.Windows.MessageBox.Show("Произошла ошибка!" + ex.Message + ex.StackTrace);
                    }
                }));
            }
        }
    }
}
