using JSON_Editor.Models;
using JSON_Editor.Views;
using System.Collections.ObjectModel;
using Microsoft.Win32;


namespace JSON_Editor.ViewModels
{
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

            //Bind Delegate
            PathWindow.OnLocationChosen = (string ChosenLocation) =>
            {
                if (ChosenLocation != "")
                {
                    //Close if path is valid
                    PathWindow.Close();
                    return;
                }
            };
            
            //Show New Window
            PathWindow.ShowDialog();
        }
        public void Open_JSON(object args = null)
        {
            OpenFileDialog FileDialog = new OpenFileDialog();
            FileDialog.ShowDialog();
            string file = FileDialog.FileName;
        }

        ObservableCollection<Asset> assets { get; set; }
        
        private Asset selectedAsset;
        public Asset SelectedAsset
        {
            get { return SelectedAsset; }
            set 
            { 
                SelectedAsset = value;
                OnPropertyChanged();
            }
        }
    }
}