using JSON_Editor.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace JSON_Editor.ViewModels
{
    public class JSON_EditorViewModel : ViewModelBase
    {
        protected FileStream? JSON_FileStream { private get;  set; }
        public JSON_EditorViewModel(string FilePath) 
        {
            //Create an instance of the observable collection
            JSONItems = new ObservableCollection<Asset>();

            FileStream JSON = File.OpenWrite(FilePath);
            
            //Task<Asset> obj = await JsonSerializer.DeserializeAsync<Asset>(JSON);

            string[] a = { "a", "b" };
            JSONItems.Add(new Asset("name1", "jpg", "path1", 50, a));
            JSONItems.Add(new Asset("name1", "jpg", "path1", 50, a));
            JSONItems.Add(new Asset("name1", "jpg", "path1", 50, a));
            JSONItems.Add(new Asset("name1", "jpg", "path1", 50, a));
            JSONItems.Add(new Asset("name1", "jpg", "path1", 50, a));
            JSONItems.Add(new Asset("name1", "jpg", "path1", 50, a));
            JSONItems.Add(new Asset("name1", "jpg", "path1", 50, a));
        }

        public ObservableCollection<Asset>? JSONItems { get; set; }
    }
}
