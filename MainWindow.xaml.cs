using JSON_Editor.ViewModels;
using System.Windows;

namespace JSON_Editor
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            DataContext = new MainViewModel();
            InitializeComponent();
        }
    }
}
