using JSON_Editor.Models;
using JSON_Editor.Views;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;


namespace JSON_Editor.ViewModels
{

    public class FileNameValidator
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

    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            //Create an instance of the observable collection
            assets = new ObservableCollection<Asset>();
            string[] a = { "a", "b" };
            
            assets.Add(new Asset("name1", "jpg", "path1", 50, a));


            CreateJSON_Event = new RelayCommand(Create_JSON);
            OpenJSON_Event = new RelayCommand(Open_JSON);
        }


        public RelayCommand CreateJSON_Event { get; }
        public RelayCommand OpenJSON_Event { get; }

        public void Create_JSON(object args = null)
        {
            //Instantiate a window that returns a path (the location the user wants to store the JSON)
            PathSelectWindow PathWindow = new PathSelectWindow();
            
            //Show New Window
            PathWindow.ShowDialog();
        }
        public void Open_JSON(object args = null)
        {
            OpenFileDialog FileDialog = new OpenFileDialog();
            FileDialog.ShowDialog();

            FileStream Json = null;

            if (File.Exists(FileDialog.FileName))
                Json = File.OpenWrite(FileDialog.FileName);

            //TODO - Open JSON Editor
            //TODO - Close All other windows
        }

        ObservableCollection<Asset> assets { get; set; }
    }
}