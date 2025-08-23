
using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace JSON_Editor.ViewModels
{
    internal class PathSelectViewModel : ViewModelBase
    {

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

        public Action RequestWindowClose;

        public PathSelectViewModel()
        {
            OnBrowse_Event = new RelayCommand(OnBrowse);
            OnAccept_Event = new RelayCommand(OnAccept);

        }
        public RelayCommand OnBrowse_Event { get; }
        public RelayCommand OnAccept_Event { get; }

        public void OnBrowse(object args = null)
        {           

            System.Windows.Forms.FolderBrowserDialog openFileDlg = new System.Windows.Forms.FolderBrowserDialog();
            var result = openFileDlg.ShowDialog();
            if (result.ToString() != string.Empty)
            {
                
                PathText = openFileDlg.SelectedPath;
            }
        }
        public void OnAccept(object args = null)
        {
            if (_PathText != "")
            {
                FileStream Json = null;
                string FilePath = Path.Combine(_PathText, (_FileNameText + ".json"));

                //Create JSON in the provided Location
                if (!File.Exists(FilePath))
                    Json = File.Create(FilePath);

                RequestWindowClose.Invoke();

                //TODO - Open JSON Editor Window
                //TODO - Close all windows except the JSON editor
            }
        }
    }
}
