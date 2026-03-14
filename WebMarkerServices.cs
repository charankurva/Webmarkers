using System;
using System.Collections.Generic;
using System.Text;

namespace Webmarkers
{
    public class WebMarkerServices
    {
        public List<WebMarker> webMarkers = new();

        public void AddWebMarker(string webname,string url,string category) {
            webMarkers.Add(new WebMarker(webname, url,category));
        }
        public void AddWebMarker(string[] name, string[] url, string[] category)
        {
            for (int i = 0; i < name.Length; i++)
            {
                webMarkers.Add(new WebMarker(name[i], url[i], category[i]));

            }
        }
        public void RemoveWebMarker(int index) { webMarkers.RemoveAt(index); }

        public List<WebMarker> ListWebMarker()
        {
            return webMarkers;

        }
        public void EditWebMarker(WebMarker webMarker,string title,string url,string category) {
            webMarker.WebName = title;
            webMarker.WebUrl = url;
            webMarker.Category=category;
        }

    }
}
