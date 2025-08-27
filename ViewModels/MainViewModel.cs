using JSON_Editor.Views;
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

        #region Actions
        public Action? RequestWindowClose;
        #endregion

        #region Relays
        public RelayCommand? CreateJSON_Event { get; }
        public RelayCommand? OpenJSON_Event { get; }

        #region Bound Functions
        public void Create_JSON(object? args = null)
        {
            //Instantiate a window that returns a path (the location the user wants to store the JSON)
            PathSelectWindow PathWindow = new PathSelectWindow();

            //Bind Delegate that closes the main window when json data is valid (to continue to the next screen)
            PathSelectViewModel VM = (PathSelectViewModel)PathWindow.DataContext;
            if (VM != null)
            {
                VM.OnWindowClosed += (EWindowCloseMethod Method) =>
                {
                    //Close the main window
                    RequestWindowClose?.Invoke();
                };
            }

            //Show New Window
            PathWindow.ShowDialog();
        }
        public void Open_JSON(object? args = null)
        {
            Microsoft.Win32.OpenFileDialog FileDialog = new Microsoft.Win32.OpenFileDialog();
            FileDialog.ShowDialog();

            if (File.Exists(FileDialog.FileName))
            {
                //Open JSON Editor
                JSON_EditorWindow Editor = new JSON_EditorWindow(FileDialog.FileName);
                Editor.Show();

                //Close the main window
                RequestWindowClose?.Invoke(); 
            }
        }

        #endregion

        #endregion
    }
}