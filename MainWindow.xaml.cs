using JSON_Editor.Views;
using Microsoft.Win32;
using System.Windows;

namespace JSON_Editor
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_OpenJSON(object sender, RoutedEventArgs e)
        {
            OpenFileDialog FileDialog = new OpenFileDialog();

            FileDialog.ShowDialog(this);
            string file = FileDialog.FileName;
        }

        private void Button_CreateJSON(object sender, RoutedEventArgs e)
        {
            //Instantiate a window that returns a path (the location the user wants to store the JSON)
            PathSelectWindow PathWindow = new PathSelectWindow();
            
            //Bind Delegate
            PathWindow.OnLocationChosen = (string path) =>
            {
                if (path != "")
                {

                }
            };

            PathWindow.Show();
        }
    }
}
