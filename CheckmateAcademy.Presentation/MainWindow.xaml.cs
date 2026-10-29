
using System.Windows;
using CheckmateAcademy.Presentation.ViewModels;

namespace CheckmateAcademy.Presentation
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
