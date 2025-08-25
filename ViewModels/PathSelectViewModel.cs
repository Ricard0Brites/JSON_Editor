using JSON_Editor.Views;
using System.IO;
using System.Windows;

namespace JSON_Editor.ViewModels
{
    enum EWindowCloseMethod
    {
        None, 
        Accept,
        Close
    }
    internal class PathSelectViewModel : ViewModelBase
    {
        #region Delegates

        //Its called when we actively close the window. It's not called when the user closes the window manually.
        public delegate void OnWindowClosedDelegate(EWindowCloseMethod Method);
        public OnWindowClosedDelegate? OnWindowClosed;

        #endregion

        private class FileNameValidator
        {
            private static readonly string[] ReservedNames =
            {
                "CON", "PRN", "AUX", "NUL",
                "COM1","COM2","COM3","COM4","COM5","COM6","COM7","COM8","COM9",
                "LPT1","LPT2","LPT3","LPT4","LPT5","LPT6","LPT7","LPT8","LPT9"
            };
            public static bool IsValidFileName(string fileName)
            {
                if (string.IsNullOrWhiteSpace(fileName))
                    return false;

                // Invalid characters
                char[] invalidChars = Path.GetInvalidFileNameChars();
                if (fileName.Any(c => invalidChars.Contains(c)))
                    return false;

                // Reserved names (case-insensitive)
                if (ReservedNames.Contains(fileName.ToUpperInvariant()))
                    return false;

                return true;
            }
        }

        public string PathText { get => _PathText; set 
            {
                if(_PathText != value)
                {
                    _PathText = value;
                    OnPropertyChanged();
                }
            } 
        }
        private string _PathText = "";

        public string FileNameText
        {
            get => _FileNameText; set
            {
                if (_FileNameText != value)
                {
                    _FileNameText = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _FileNameText = "";

        public Action? RequestWindowClose;
        
        public PathSelectViewModel()
        {
            OnBrowse_Event = new RelayCommand(OnBrowse);
            OnAccept_Event = new RelayCommand(OnAccept);
           
        }
        public RelayCommand? OnBrowse_Event { get; }
        public RelayCommand? OnAccept_Event { get; }

        public void OnBrowse(object? args = null)
        {           

            FolderBrowserDialog openFileDlg = new FolderBrowserDialog();
            var result = openFileDlg.ShowDialog();
            if (result.ToString() != string.Empty)
            {
                
                PathText = openFileDlg.SelectedPath;
            }
        }
        public void OnAccept(object? args = null)
        {
            if (_PathText != "")
            {
                //Validate File Name
                if (!FileNameValidator.IsValidFileName(_FileNameText))
                {
                    MessageBoxResult result = System.Windows.MessageBox.Show("Invalid File Name.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                //Validate File Path
                char[] InvalidChars;
                {
                    //This is inside braces to free memory
                    char[] DefaultInvalidChars = Path.GetInvalidPathChars();
                    char[] ExtraCharExclusionList = { ' ' };
                    InvalidChars = DefaultInvalidChars.Concat(ExtraCharExclusionList).ToArray();
                }

                if ( _PathText.IndexOfAny(InvalidChars) > 0)
                {
                    MessageBoxResult result = System.Windows.MessageBox.Show("Invalid Path.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                //Check if the path exists or of we need to create it
                if(!Directory.Exists(_PathText))
                    Directory.CreateDirectory(_PathText);

                //Create JSON in the provided Location
                FileStream? Json = null;
                Json = File.Create(Path.Combine(_PathText, (_FileNameText + ".json")));                
                Json.Close();

                JSON_EditorWindow JSONEditor = new JSON_EditorWindow(Json.Name);
                JSONEditor.Show();

                RequestWindowClose?.Invoke();
                OnWindowClosed?.Invoke(EWindowCloseMethod.Accept);
            }
        }
    }
}
