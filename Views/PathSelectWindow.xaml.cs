using JSON_Editor.ViewModels;
using System.Windows;

namespace JSON_Editor.Views
{
    public partial class PathSelectWindow : Window
    {
        public PathSelectWindow()
        {
            var VM = new PathSelectViewModel();
            DataContext = VM;

            VM.RequestWindowClose += () => Close();
            
            
            
            InitializeComponent();
        }
    }
}
