using JSON_Editor.ViewModels;
using System.Text.Json.Serialization;

namespace JSON_Editor.Models
{
    public class Asset : ViewModelBase
    {
        //Parameters
        private string? _Name;
        public string Name
        {
            get => _Name == null ? "" : _Name;
            set
            {
                if (_Name == value)
                    return;
                _Name = value;
                if (!_HasInit[0])
                {
                    _HasInit[0] = true;
                }
                else
                    _IsDirty = true;
                OnPropertyChanged();
            }
        }
        private string? _Type;
        public string Type
        {
            get => _Type == null ? "" : _Type;
            set
            {
                if (_Type == value)
                    return;
                _Type = value;
                if (!_HasInit[1])
                {
                    _HasInit[1] = true;
                }
                else
                    _IsDirty = true;
                OnPropertyChanged();
            }
        }
        private string? _Path;
        public string Path
        {
            get => _Path == null ? "" : _Path;
            set
            {
                if (_Path == value)
                    return;
                _Path = value;
                if (!_HasInit[2])
                {
                    _HasInit[2] = true;
                }
                else
                    _IsDirty = true;
                OnPropertyChanged();
            }
        }
        private float? _SizeB;
        public float? SizeB
        {
            get => _SizeB == null ? 0 : _SizeB;
            set
            {
                if (_SizeB == value)
                    return;
                _SizeB = value;
                if (!_HasInit[3])
                {
                    _HasInit[3] = true;
                }
                else
                    _IsDirty = true;
                OnPropertyChanged();
            }
        }
        public List<string> Tags { get; set; }

        public bool IsDirty() { return _IsDirty; }

        //This function should only be called when the json is saved
        public void Clean() { _IsDirty = false; }

        [JsonIgnore]
        public string TagsDisplay { get => GetTagsReadable(); set => SetTags(value); }

        public Asset(string name, string type, string path, float sizeMB, List<string> tags, bool IsDirty)
        {
            Name = name;
            Type = type;
            Path = path;
            SizeB = sizeMB;
            Tags = tags;
            _IsDirty = IsDirty;
        }
        public Asset()
        {
            Name = "";
            Type = "";
            Path = "";
            SizeB = 0;
            Tags = new List<string>([]);
            _IsDirty = true;
        }

        /* -------------------- */

        private string GetTagsReadable()
        {
            if (Tags == null || Tags.Count == 0)
                return string.Empty;

            return string.Join(", ", Tags);
        }
        private void SetTags(string? T)
        {
            if (T != null && T.Length == 0)
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
            if (!_HasInit[4])
            {
                _HasInit[4] = true;
            }
            else
                _IsDirty = true;
            _IsDirty = true;
            OnPropertyChanged();
        }

        private bool _IsDirty = false;
        private bool[] _HasInit = new bool[] { false, false, false, false, false };
    }
}
