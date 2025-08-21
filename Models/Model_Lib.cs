namespace JSON_Editor.Models
{
    public class Asset
    {
       public string Name { get; set; }
        public string  Type { get; set; }
        public string  Path { get; set; }
        public float  SizeMB { get; set; }
        public string[]  Tags { get; set; }

        public Asset(string name, string type, string path, float sizeMB, string[] tags)
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
            Tags = new string[0];
        }
    }
}
