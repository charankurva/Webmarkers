using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;


namespace Webmarkers
{
    public class WebMarkerServices:IWebMarker
    {
        private List<WebMarker> _webMarkers = new();
        public List<WebMarker> WebMarkers
        {
            get => _webMarkers;
            set => _webMarkers = value ?? new(); // Prevent nulls!
        }

        //public List<WebMarker> WebMarkers => throw new NotImplementedException();

        public void AddWebMarker(string name, string url, string category)
        {
            _webMarkers.Add(new WebMarker(name, url, category));
            
        }
        public void AddWebMarker(string[] name, string[] url, string[] category)
        {
            //linq query is to get the names of webmarks
            IEnumerable<string> Markers = from x in _webMarkers
                                             select x.WebName;
            
            for (int i = 0; i < name.Length; i++)
            {
                if (!Markers.Contains(name[i])) { 
                _webMarkers.Add(new WebMarker(name[i], url[i], category[i]));
                    Console.WriteLine("Added sucessfully");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You cannot enter the same bookmark again");
                    Console.ResetColor();
                }
            }
        }
        public void RemoveWebMarker(int index) {

            _webMarkers.RemoveAt(index-1); 
        }

        public void ListWebMarker(string? category)
        {
            if (string.IsNullOrEmpty(category)) {
                IEnumerable <WebMarker> result= from m in _webMarkers
                                                where m.Category == category
                                                 select m;
                _webMarkers = result.ToList();

            }
            int count = 0;
            Console.ForegroundColor= ConsoleColor.Green;
            foreach (WebMarker mark in _webMarkers)
            {
                count++;
                Console.WriteLine($"{count}) {mark.WebName} | {mark.WebUrl} |{mark.Category}");
            }
            Console.ResetColor();
                

        }
        public void EditWebMarker(WebMarker webMarker,string title,string url,string category) {
            webMarker.WebName = title;
            webMarker.WebUrl = url;
            webMarker.Category=category;
        }

        public void ImportWebMarker(List<WebMarker> _webMarker,FileInfo _file) {
            
            IEnumerable<WebMarker> result = from m in _webMarker
                                            from m2 in _webMarkers
                                            where m.WebUrl != m2.WebUrl
                                            select m;
            int count = 0;
            foreach (WebMarker webMarker in result) {
                _webMarkers.Add(new WebMarker(webMarker.WebName,webMarker.WebUrl,webMarker.Category));
                count++;
                //Console.WriteLine($"{count}) {webMarker.WebName} | {webMarker.WebUrl} |{webMarker.Category}");
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{count} webmarks are added");
            Console.ResetColor();

        } 

        public void FileReader(FileInfo file)
        {
            var content = File.ReadAllText(file.FullName);
            if (!string.IsNullOrEmpty(content))
            {
                List<WebMarker> Markers = JsonSerializer.Deserialize<List<WebMarker>>(content);
                _webMarkers = Markers;
            }
        }

        public void FileWriter(FileInfo file)
        {
              var json = JsonSerializer.Serialize(_webMarkers, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(file.FullName, json);
            
        }

        public void UpdateWebMarker(int idval,string url)
        {
            _webMarkers[idval - 1].WebUrl= url;
        }

        public FileInfo InitializeFile(string path)
        {
            FileInfo json_file = new FileInfo(path);
            if (!File.Exists(json_file.FullName)){
                File.Create(json_file.FullName);
            }
            return json_file;
        }
    }
}
