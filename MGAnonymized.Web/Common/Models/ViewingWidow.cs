using System;

namespace MGAnonymized.Web.Common.Models
{
    public class ViewingWindow
    {
        public DateTime StartDate { get; set; }

        public string WayToWatch { get; set; }

        public DateTime EndDate { get; set; }

        public string Title { get; set; }
    }
}