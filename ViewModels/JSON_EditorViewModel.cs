using JSON_Editor.Models;
using System.Collections.ObjectModel;
using System.IO;

namespace JSON_Editor.ViewModels
{
    public class JSON_EditorViewModel : ViewModelBase
    {
        protected FileStream JSON_FileStream { private get;  set; }
        public JSON_EditorViewModel() 
        {
            //Create an instance of the observable collection
            JSONItems = new ObservableCollection<Asset>();
            
            
            string[] a = { "a", "b" };
            JSONItems.Add(new Asset("name1", "jpg", "path1", 50, a));
            JSONItems.Add(new Asset("name1", "jpg", "path1", 50, a));
            JSONItems.Add(new Asset("name1", "jpg", "path1", 50, a));
            JSONItems.Add(new Asset("name1", "jpg", "path1", 50, a));
            JSONItems.Add(new Asset("name1", "jpg", "path1", 50, a));
            JSONItems.Add(new Asset("name1", "jpg", "path1", 50, a));
            JSONItems.Add(new Asset("name1", "jpg", "path1", 50, a));
        }

        public ObservableCollection<Asset> JSONItems { get; set; }
    }
}
