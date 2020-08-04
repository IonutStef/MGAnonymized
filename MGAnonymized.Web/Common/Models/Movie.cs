using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MGAnonymized.Web.Common.Models
{
    public class Movie
    {
        [JsonProperty(PropertyName = "Body")]
        public string Description { get; set; }

        public List<CardImage> CardImages { get; set; }

        public List<Cast> Cast { get; set; }

        public string Cert { get; set; }

        public string Class { get; set; }

        public List<Director> Directors { get; set; }

        public int Duration { get; set; }

        public List<string> Genres { get; set; }

        public string Headline { get; set; }

        public string Id { get; set; }

        public List<KeyArtImage> KeyArtImages { get; set; }

        public DateTime LastUpdated { get; set; }

        public string Quote { get; set; }

        public int Rating { get; set; }

        public string ReviewAuthor { get; set; }

        public string SkyGoId { get; set; }

        public string SkyGoUrl { get; set; }

        public string Sum { get; set; }

        public string Synopsis { get; set; }

        public string Url { get; set; }

        public List<Video> Videos { get; set; }

        public ViewingWindow ViewingWindow { get; set; }

        public string Year { get; set; }

        public List<Gallery> Galleries { get; set; }

        public string SgId { get; set; }

        public string SgUrl { get; set; }

        public bool ImagesSavedLocally { get; set; }
    }
}