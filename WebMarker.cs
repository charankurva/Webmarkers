using System;
using System.Collections.Generic;
using System.Text;

namespace Webmarkers
{
    public class WebMarker
    {
        public string WebName { get; set; }
        public string WebUrl { get; set; }

        public string Category { get; set; }

        public WebMarker(string webName, string webUrl, string category) { 
            this.WebName = webName;
            this.WebUrl = webUrl;
            this.Category = category;
        
        }

    }
}
