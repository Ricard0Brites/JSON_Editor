using JSON_Editor.Models;
using JSON_Editor.Views;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;


namespace JSON_Editor.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            #region Relay Bindings
            
            CreateJSON_Event = new RelayCommand(Create_JSON);

            OpenJSON_Event = new RelayCommand(Open_JSON);

            #endregion
        }

        #region Relays
        public RelayCommand CreateJSON_Event { get; }
        public RelayCommand OpenJSON_Event { get; }

            #region Bound Functions
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

            #endregion

        #endregion
    }
}