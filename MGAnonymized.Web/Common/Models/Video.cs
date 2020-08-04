using System.Collections.Generic;

namespace MGAnonymized.Web.Common.Models
{
    public class Video
    {
        public string Title { get; set; }

        public List<AlternativeVideo> Alternatives { get; set; }

        public string Type { get; set; }

        public string Url { get; set; }

        public string ThumbnailUrl { get; set; }
    }
}