
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Controls;
using CheckmateAcademy.Presentation.Views;

namespace CheckmateAcademy.Presentation.ViewModels
{
    public sealed class MainViewModel : INotifyPropertyChanged
    {
        private object? _currentView;
        private string _pageTitle = "Главная";
        private string _pageSubtitle = "Твой путь к шахматному мастерству";

        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand NavigateCommand { get; }

        public object? CurrentView
        {
            get => _currentView;
            private set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public string PageTitle
        {
            get => _pageTitle;
            private set
            {
                _pageTitle = value;
                OnPropertyChanged();
            }
        }

        public string PageSubtitle
        {
            get => _pageSubtitle;
            private set
            {
                _pageSubtitle = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            NavigateCommand = new RelayCommand(Navigate);
            Navigate("Home");
        }

        private void Navigate(object? parameter)
        {
            if (parameter is not string page)
                return;

            switch (page)
            {
                case "Home":
                    PageTitle = "Главная";
                    PageSubtitle = "Твой путь к шахматному мастерству";
                    CurrentView = new HomeView();
                    break;

                case "Courses":
                    PageTitle = "Курсы";
                    PageSubtitle = "Изучай теорию и закрепляй знания на практике";
                    CurrentView = new CoursesView();
                    break;

                case "Tasks":
                    PageTitle = "Задачи";
                    PageSubtitle = "Развивай тактическое мышление";
                    CurrentView = new TasksView();
                    break;

                case "Bot":
                    PageTitle = "Игра с ботом";
                    PageSubtitle = "Тренируйся против компьютерных соперников";
                    CurrentView = new BotView();
                    break;

                case "Study":
                    PageTitle = "Анализ партий";
                    PageSubtitle = "Исследуй позиции и разбирай варианты ходов";
                    CurrentView = new StudyView();
                    break;
            }
        }

        private void OnPropertyChanged(
            [CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }
    }

    public sealed class RelayCommand : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Predicate<object?>? _canExecute;

        public event EventHandler? CanExecuteChanged;

        public RelayCommand(
            Action<object?> execute,
            Predicate<object?>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }

        public void Execute(object? parameter)
        {
            _execute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
