using JSON_Editor.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace JSON_Editor.ViewModels
{
    public class JSON_EditorViewModel : ViewModelBase
    {
        public JSON_EditorViewModel(string FilePath) 
        {
            //Create an instance of the observable collection
            JSONItems = new ObservableCollection<Asset>();

            #region File Management

            //Open The provided File (read/write)
            FileStream? JSON = null;
            try
            {
                JSON = File.Open(FilePath, FileMode.Open);
            }
            catch(Exception Error)
            {
                Debug.WriteLine(Error);
                return; // Stop execution, file is invalid
            }

            // Read the file
            if (JSON?.Length > 0)
            {
                try
                {
                    Asset[]? obj = JsonSerializer.Deserialize<Asset[]>(JSON);

                    if (obj == null)
                        return;

                    foreach (Asset A in obj)
                    {
                        //TODO - Add the contents of the Asset to "JSONItems" 
                        //Like so: "JSONItems.Add(new Asset("name1", "jpg", "path1", 50, Tags[]));"
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }

            #endregion

            #region TODO - DELETE

            List<string> a = ["a", "bsdbflsdbfjhsdbfjhsdbfjhsdbfjhsbdfjhsbjhdfbsjhdfbjhsbfjhsbdfjhsbdfjhbsadkjhbskfjahsbfkjhdbfkjashdbfkjashdbfkjashdbfkjashdbfjkashdbfakjsdhfbaksjdhfabsdkjfb"];
            JSONItems.Add(new Asset("name1", "jpg", "path1", 50, a));
            JSONItems.Add(new Asset("name1", "jpg", "path1", 50, a));
            JSONItems.Add(new Asset("name1", "jpg", "path1", 50, a));
            JSONItems.Add(new Asset("name1", "jpg", "path1", 50, a));
            JSONItems.Add(new Asset("name1", "jpg", "path1", 50, a));
            JSONItems.Add(new Asset("name1", "jpg", "path1", 50, a));
            JSONItems.Add(new Asset("name1", "jpg", "path1", 50, a)); 

            #endregion

            //Start an auto-saving timer
            StartAutoSaveTimer();
        }
        public ObservableCollection<Asset>? JSONItems { get; set; }

        public int NumOfItems => JSONItems?.Count ?? 0;

        //This event is bound in code-behind and called on window close event trigger
        public void OnTryClosingWindow(object? Obj, CancelEventArgs? CancelEventArgs)
        {
            /*
             * TODO: 
             *  Is Dirty?
             *      Yes: Create pop-up asking user to save unsaved changes (yes/no)
             *      No: Do nothing and let program stop
             */
                
        }

        #region Save Timer
        private void StartAutoSaveTimer()
        {
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 5, 0);
            dispatcherTimer.Start();
        }
        private void dispatcherTimer_Tick(object? sender, EventArgs? e)
        {
            //TODO - Save changes to JSON
        }
        #endregion

    }
}
