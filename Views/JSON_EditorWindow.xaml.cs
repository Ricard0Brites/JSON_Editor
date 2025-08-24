using System.Windows;
using JSON_Editor.ViewModels;

namespace JSON_Editor.Views
{
    public partial class JSON_EditorWindow : Window
    {
        public JSON_EditorWindow(string FilePath)
        {
            DataContext = new JSON_EditorViewModel(FilePath);
            InitializeComponent();
        }
    }
}
