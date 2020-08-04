using Newtonsoft.Json;

namespace MGAnonymized.Web.Common.Models
{
    public class Image
    {
        public string Url { get; set; }

        [JsonProperty(PropertyName = "H")]
        public int Height { get; set; }

        [JsonProperty(PropertyName = "W")]
        public int Width { get; set; }

        [JsonIgnore]
        public string ShortName
        {
            get
            {
                var imageNameStartPossition = Url.LastIndexOf("/") + 1;
                return Url.Substring(imageNameStartPossition, Url.Length - imageNameStartPossition);
            }
        }
        public string LocalPath { get; set; }
    }
}