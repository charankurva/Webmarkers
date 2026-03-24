using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace Webmarkers
{
    public class WebMarkerSettings
    {
        private const string DefaultFileNmae = "WebMarker.json";
        public  string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DefaultFileNmae);
        
       
        
    }
}
