using JSON_Editor.ViewModels;

namespace JSON_Editor.Models
{
    public class Asset : ViewModelBase
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Path { get; set; }
        public float SizeMB { get; set; }
        public List<string> Tags { get; set; }

        public string TagsDisplay { get => GetTagsReadable(); set => SetTags(value); }

        public Asset(string name, string type, string path, float sizeMB, List<string> tags)
        {
            Name = name;
            Type = type;
            Path = path;
            SizeMB = sizeMB;
            Tags = tags;
        }
        public Asset()
        {
            Name = "";
            Type = "";
            Path = "";
            SizeMB = 0;
            Tags = new List<string>([]);
        }
        private string GetTagsReadable()
        {
            if (Tags == null || Tags.Count == 0)
                return string.Empty;

            return string.Join(", ", Tags);
        }
        private void SetTags(string? T)
        {
            if(T != null && T.Length == 0)
            {
               Tags = [];
            }

            List<string> UpdatedTags = new List<string>([]);
            string TagCache = "";

            for (int i = 0; i < T?.Length; ++i)
            {
                //Remove Spaces
                if (T[i] == ' ') 
                    continue;

                //Tag Separator
                if (T[i] == ',')
                {
                    UpdatedTags.Add(TagCache);
                    TagCache = "";
                    continue;
                }

                //Append Char to cache
                TagCache += T[i];

                //Add Last Tag
                if (i == T.Length - 1)
                    UpdatedTags.Add(TagCache);
            }

            Tags = UpdatedTags;
            OnPropertyChanged();
        }
    }
}
