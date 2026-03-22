using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;


namespace Webmarkers
{
    public class WebMarkerServices
    {
        public List<WebMarker> webMarkers = new();
        
        public void AddWebMarker(string[] name, string[] url, string[] category)
        {
            //linq query is to get the names of webmarks
            IEnumerable<string> Markers = from x in webMarkers
                                             select x.WebName;
            

            for (int i = 0; i < name.Length; i++)
            {
                if (!Markers.Contains(name[i])) { 
                webMarkers.Add(new WebMarker(name[i], url[i], category[i]));
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

            webMarkers.RemoveAt(index-1); 
        }

        public void ListWebMarker(string[] category)
        {
            if (category.Length>0) {
                IEnumerable <WebMarker> result= from m in webMarkers
                                                 where m.Category == category[0]
                                                 select m;
                webMarkers = result.ToList();

            }
            int count = 0;
            Console.ForegroundColor= ConsoleColor.Green;
            foreach (WebMarker mark in webMarkers)
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

        public void ImportWebMarker(List<WebMarker> _webMarker) {
            IEnumerable<WebMarker> result = from m in _webMarker
                                            from m2 in webMarkers
                                            where m.WebUrl != m2.WebUrl
                                            select m;
            int count = 0;
            foreach (WebMarker webMarker in result) {
                webMarkers.Add(new WebMarker(webMarker.WebName,webMarker.WebUrl,webMarker.Category));
                count++;
                Console.WriteLine($"{count}) {webMarker.WebName} | {webMarker.WebUrl} |{webMarker.Category}");
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
                webMarkers = Markers;
            }
        }

        public void FileWriter(FileInfo file)
        {
              var json = JsonSerializer.Serialize(webMarkers, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(file.FullName, json);
            
        }

        public void UpdateWebMarker(int idval,string[] url)
        {
            webMarkers[idval - 1].WebUrl= url[0];
        }
    }
}
