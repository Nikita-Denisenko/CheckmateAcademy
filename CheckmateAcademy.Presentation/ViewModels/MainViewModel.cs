
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
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

            var view = page switch
            {
                "Home" => (object)new HomeView(),
                "Courses" => new CoursesView(),
                "Tasks" => new TasksView(),
                "Bot" => new BotView(),
                "Study" => new StudyView(),
                _ => null
            };

            if (view is null)
                return;

            (PageTitle, PageSubtitle) = page switch
            {
                "Home" => ("Главная", "Твой путь к шахматному мастерству"),
                "Courses" => ("Курсы", "Изучай теорию и закрепляй знания на практике"),
                "Tasks" => ("Задачи", "Развивай тактическое мышление"),
                "Bot" => ("Игра с ботом", "Тренируйся против компьютерных соперников"),
                "Study" => ("Анализ партий", "Исследуй позиции и разбирай варианты ходов"),
                _ => (PageTitle, PageSubtitle)
            };

            CurrentView = view;
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
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
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
