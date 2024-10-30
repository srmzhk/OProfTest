using System;
using Microsoft.Office.Interop.Word;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using OProfTest.MVVM.Model;
using OProfTest.MVVM.View;
using OProfTest.Repositories;
using System.Windows.Controls;
using Application = Microsoft.Office.Interop.Word.Application;
using System.Windows.Shapes;

namespace OProfTest.MVVM.ViewModel
{
    internal class ResultWindowViewModel : ObservableObject
    {
        private readonly ResultsRepository _resultsRepository;

        private User _currentUser;
        private Result _currentResult;

        public ResultWindowViewModel(Result result, User user) {
            _resultsRepository = new ResultsRepository();

            _currentResult = result;
            _currentUser = user;
            _resultsRepository.AddResult(_currentResult);
            Description = _currentResult.Description;
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

        private readonly RelayCommand _backToTests;
        public RelayCommand BackToTests
        {
            get
            {
                return _backToTests ?? (new RelayCommand(obj =>
                {
                    try
                    {
                        var win = new TestsCatalog(_currentUser);
                        App.Current.Windows[0].Close();
                        win.Show();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Что-то пошло не так..." + ex.Message);
                    }
                }));
            }
        }
        private readonly RelayCommand _openExplanation;
        public RelayCommand OpenExplanation
        {
            get
            {
                return _openExplanation ?? (new RelayCommand(obj =>
                {
                    try
                    {
                        System.Diagnostics.Process.Start(_currentResult.FilePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Что-то пошло не так..." + ex.Message);
                    }
                }));
            }
        }
    }
}
