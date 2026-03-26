using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Runtime;
using System.Text;

namespace Webmarkers.Commands
{
    public class AddCommand:Command
    {
        #region property
        private readonly WebMarkerSettings _settings;
        private readonly IWebMarker _service;
        #endregion
        public AddCommand(IWebMarker service, WebMarkerSettings settings) :base("add","adding a webmark")
        {
            _settings = settings;
            _service = service;
            Add(nameoption);
            Add(urloption);
            Add(categoryoption);
            categoryoption.AcceptOnlyFromAmong("movie", "learning", "anime");
            SetAction((parseResult) =>
            {
                string name = parseResult.GetValue(nameoption);
                string url = parseResult.GetValue(urloption);
                string category = parseResult.GetValue(categoryoption);
                service.AddWebMarker(name, url, category);
                service.FileWriter(_service.InitializeFile(_settings.FilePath));
               

            });

        }
        #region option
        private Option<string> nameoption = new Option<string>("--name", "-n") { Description = "takes the website name", Required = true, AllowMultipleArgumentsPerToken = true };
        private Option<string> urloption = new Option<string>("--url", "-u") { Description = "takes the website url", Required = true, AllowMultipleArgumentsPerToken = true };
        private Option<string> categoryoption = new Option<string>("--category", "-c") { Description = "takes the website catogory", Required = false, AllowMultipleArgumentsPerToken = true };
        #endregion

        #region configuration of options

        public void Configuration()
        {
            
            urloption.Validators.Add((result) => {
                foreach (var token in result.Tokens)
                {
                    
                    if(!Uri.TryCreate(token.Value, UriKind.Absolute, out _))
                    {
                        result.AddError($"Invalid url:{token.Value}");
                        break;
                    }

                }

                
            
            
            });
        }

        #endregion

    }
}
