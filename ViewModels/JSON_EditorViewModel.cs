using JSON_Editor.Models;
using JSON_Editor.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Shapes;

namespace JSON_Editor.ViewModels
{
    public class JSON_EditorViewModel : ViewModelBase
    {
        public JSON_EditorViewModel(string FilePath) 
        {
            //Create an instance of the observable collection
            JSONItems = new ObservableCollection<Asset>();

            if (OpenFile(FilePath))
                TryReadJSONFile();
            else
                return;

            #region TODO - DELETE

//             List<string> a = ["a", "bsdbflsdbfjhsdbfjhsdbfjhsdbfjhsbdfjhsbjhdfbsjhdfbjhsbfjhsbdfjhsbdfjhbsadkjhbskfjahsbfkjhdbfkjashdbfkjashdbfkjashdbfkjashdbfjkashdbfakjsdhfbaksjdhfabsdkjfb"];
//             JSONItems.Add(new Asset("flsdbfjhsdbfjhsdbfjhsdbfjhsbdfjhsbjhdfbsjhdfbjhsbfjhsbdfjhsbdfjhbsadkjhbskfjahsbfkjhdbfkjashdbfkjashdbfkjashdbfkjashd", "jpg", "path1", 50, a));
//             JSONItems.Add(new Asset("name1", "flsdbfjhsdbfjhsdbfjhsdbfjhsbdfjhsbjhdfbsjhdfbjhsbfjhsbdfjhsbdfjhbsadkjhbskfjahsbfkjhdbfkjashdbfkjashdbfkjashdbfkjashd", "path1", 50, a));
//             JSONItems.Add(new Asset("name1", "jpg", "flsdbfjhsdbfjhsdbfjhsdbfjhsbdfjhsbjhdfbsjhdfbjhsbfjhsbdfjhsbdfjhbsadkjhbskfjahsbfkjhdbfkjashdbfkjashdbfkjashdbfkjashd", 50, a));
//             JSONItems.Add(new Asset("name1", "jpg", "path1", 500000000000000000, a));
//             JSONItems.Add(new Asset("name1", "jpg", "path1", 50, a));
//             JSONItems.Add(new Asset("name1", "jpg", "path1", 50, a));
//             JSONItems.Add(new Asset("name1", "jpg", "path1", 50, a));
//             JSONItems.Add(new Asset("name1", "jpg", "path1", 50, a));
//             JSONItems.Add(new Asset("name1", "jpg", "path1", 50, a));
        

            #endregion

            #region Relay Binds
            AddEntry = new RelayCommand(AddEntryCallback);
            RemoveEntry = new RelayCommand(RemoveEntryCallBack);
            RequestSave = new RelayCommand(RequestSaveCallBack);
            #endregion

            //Start an auto-saving timer
            StartAutoSaveTimer();
        }

        #region Asset Management
        private bool HasFileBeenModified = false;
        //JSON Deserialized data
        public ObservableCollection<Asset>? JSONItems { get; set; }

        private bool IsAssetPoolDirty()
        {
            if (JSONItems == null)
                return true;

            foreach (Asset A in JSONItems)
            {
                if (A.IsDirty())
                    return true;
            }
            return false;
        }

        private void CleanAssetPool()
        {
            if (JSONItems == null)
                return;

            foreach (Asset A in JSONItems)
            {
                A.Clean();
            }
        }

        #endregion

        #region Window Management

        //This event is bound in code-behind and called on window close event trigger
        public void OnTryClosingWindow(object? Obj, CancelEventArgs? CancelArgs)
        {
            if(IsAssetPoolDirty() || HasFileBeenModified)
            {

                MessageBoxResult result = System.Windows.MessageBox.Show("Would you like to save your changes?", "Unsaved Changes", MessageBoxButton.YesNoCancel, MessageBoxImage.Asterisk);

                switch(result) 
                {
                    case MessageBoxResult.Yes:
                        if (!SaveChanges(true) && CancelArgs != null)
                        {
                            CancelArgs.Cancel = true;
                            MessageBoxResult alert = System.Windows.MessageBox.Show("Changes were Not saved successfully.", "Unsaved Changes", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        break;
                    
                    case MessageBoxResult.No: // Lets the window close
                        break;

                    case MessageBoxResult.Cancel:
                        if(CancelArgs != null)
                        {
                            //Stop the window closing
                            CancelArgs.Cancel = true;
                        }
                        break;

                }
            }
        }

        #endregion

        #region Auto Save
        private void StartAutoSaveTimer()
        {
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 5, 0);
            dispatcherTimer.Start();
        }
        private void DispatcherTimer_Tick(object? sender, EventArgs? e)
        {
            SaveChanges(false);
        }

        #endregion

        #region IO
        //JSON File Stream
        private FileStream? JSONFileStream = null;

        protected bool SaveChanges(bool DisplaySaveState)
        {
            if (JSONFileStream == null || JSONItems == null)
                return false;

            if (IsAssetPoolDirty() || HasFileBeenModified)
            {
                try
                {
                    //Clear old data
                    JSONFileStream.SetLength(0);
                    JSONFileStream.Position = 0;

                    //Rewrite
                    JsonSerializer.Serialize(JSONFileStream, JSONItems);

                    JSONFileStream.Flush();

                    //Mark all entries clean
                    CleanAssetPool();
                    HasFileBeenModified = false;

                    if (DisplaySaveState)
                       System.Windows.MessageBox.Show("Saved Successfully", "Saved", MessageBoxButton.OK, MessageBoxImage.None);

                    return true;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    if (DisplaySaveState)
                        System.Windows.MessageBox.Show("Error Saving", "Not Saved", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else
                return true; // True because we didnt need to save (data was not dirty)
        }

        //Tries to open the file in the given path (with read/write)
        private bool OpenFile(string FilePath)
        {
            //Open The provided File (read/write)
            try
            {
                JSONFileStream = File.Open(FilePath, FileMode.Open);
                return true;
            }
            catch (Exception Error)
            {
                Debug.WriteLine(Error);
                MessageBoxResult alert = System.Windows.MessageBox.Show("Could not open file in the specified location.", "Stopping...", MessageBoxButton.OK, MessageBoxImage.Error);
                return false; // Stop execution, file is invalid
            }
        }

        private void TryReadJSONFile()
        {
            // Read the file
            if (JSONFileStream?.Length > 0)
            {
                try
                {
                    Asset[]? obj = JsonSerializer.Deserialize<Asset[]>(JSONFileStream);

                    if (obj == null || JSONItems == null)
                        return;

                    foreach (Asset A in obj)
                    {
                        float size = 0;

                        if (A.SizeB == null)
                        {
                            FileInfo fileInfo = new FileInfo(A.Path);
                            size = fileInfo.Length;
                        }
                        else
                            size = (float)A.SizeB;

                        JSONItems.Add(new Asset(A.Name, A.Type, A.Path, size, A.Tags, false));
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxResult alert = System.Windows.MessageBox.Show("Could not Deserialize file", "Invalid File Contents", MessageBoxButton.OK, MessageBoxImage.Error);
                    Debug.WriteLine(ex);
                }
            }
        }

        #endregion

        #region Relays

        // Adds a row to the table and in JSON        
        public RelayCommand? AddEntry { get; }

        public void AddEntryCallback(object? args = null)
        {
            if (JSONItems == null)
                return;


            Microsoft.Win32.OpenFileDialog FileDialog = new Microsoft.Win32.OpenFileDialog();
            FileDialog.ShowDialog();

            if (File.Exists(FileDialog.FileName))
            {

                FileInfo FileData = new FileInfo(FileDialog.FileName);

                JSONItems.Add(new Asset(FileDialog.SafeFileName, System.IO.Path.GetExtension(FileDialog.SafeFileName), FileDialog.FileName, FileData.Length, [], true));
            }
        }


        //Removes a row from the table and from JSON
        public RelayCommand? RemoveEntry { get; }
        
        public void RemoveEntryCallBack(object? args = null)
        {
            if(CurrentAsset != null)
            {
                JSONItems?.Remove(CurrentAsset);
                HasFileBeenModified = true;
            }
        }


        //Request Save
        public RelayCommand? RequestSave { get; }

        public void RequestSaveCallBack(object? args = null)
        {
            SaveChanges(true);
        }

        #endregion

        #region Entry Removal
        public Asset? CurrentAsset { get; set; }
        #endregion
    }
}
