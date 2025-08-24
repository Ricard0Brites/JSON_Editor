
using JSON_Editor.ViewModels;
using System.Windows;

namespace JSON_Editor
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            MainViewModel VM = new MainViewModel();
            DataContext = VM;

            //Allows to close the window from the view model
            VM.RequestWindowClose += () => Close();

            InitializeComponent();
        }
    }
}
