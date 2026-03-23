using System;
using System.Collections.Generic;
using System.Text;
namespace Webmarkers
{
    public interface IWebMarker
    {
        List<WebMarker> WebMarkers { get; set; }
        public void AddWebMarker(string[] name, string[] url, string[] category);
        public void AddWebMarker(string name, string url, string category);
        public void RemoveWebMarker(int index);
        public void EditWebMarker(WebMarker webMarker, string title, string url, string category);
        public void ImportWebMarker(List<WebMarker> _webMarker, FileInfo _file);
        public void FileReader(FileInfo file);
        public void FileWriter(FileInfo file);
        public void UpdateWebMarker(int idval, string[] url);
        public void ListWebMarker(string[]? category);
    }
}
