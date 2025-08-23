using Microsoft.Win32;
using System.Windows;

namespace JSON_Editor.Views
{
    public partial class PathSelectWindow : Window
    {

        //TODO - Refactor into MVVM
        #region Delegates

        //Triggers when the user clicks 'OK' in the browser picker
        public delegate void OnLocationChosenCallback(string path, string FileName);
        
        // Important: Sanitize Path Before Use
        public OnLocationChosenCallback OnLocationChosen;

        #endregion

        private string SavedPath = "";
        public PathSelectWindow()
        {
            InitializeComponent();
        }

        public void ShowPathPicker()
        {
            System.Windows.Forms.FolderBrowserDialog openFileDlg = new System.Windows.Forms.FolderBrowserDialog();
            var result = openFileDlg.ShowDialog();
            if (result.ToString() != string.Empty)
            {
                SavedPath = openFileDlg.SelectedPath;
                Path.Text = SavedPath;
            }
        }

        private void Button_Browse(object sender, RoutedEventArgs e)
        {
            ShowPathPicker();
        }

        private void Button_Accept(object sender, RoutedEventArgs e)
        {
            if (OnLocationChosen != null)
                OnLocationChosen.Invoke(Path.Text, FileName.Text);
        }
    }
}
