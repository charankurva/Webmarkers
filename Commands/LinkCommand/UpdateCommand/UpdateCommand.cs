using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace Webmarkers.Commands
{
    public class UpdateCommand:Command
    {
        #region properties
        private readonly IWebMarker _service;
        private readonly WebMarkerSettings _settings;
        #endregion
        public UpdateCommand(IWebMarker service, WebMarkerSettings settings) :base("update","update the url of the webmarker")
        {
            _settings= settings;
            _service = service;
            this.Options.Add(urloption);
            this.Options.Add(idOption);
            SetAction((ParseResult) =>
            {
                int idval = ParseResult.GetValue(idOption);
                string url = ParseResult.GetValue(urloption);
                if (idval != 0)
                {
                    _service.FileReader(_service.InitializeFile(_settings.FilePath));
                    _service.UpdateWebMarker(idval, url);
                    _service.FileWriter(_service.InitializeFile(_settings.FilePath));
                }

            });
        }

        #region option
        public Option<string> urloption = new Option<string>("--url", "-u") { Description = "takes the website url", Required = true, AllowMultipleArgumentsPerToken = true };
        public Option<int> idOption = new Option<int>("--id", "-i") { Required=true,Description="Id of the webmark"};

        #endregion
        
        #region configuration
        public void Configuration()
        {

            urloption.Validators.Add((result) => {
                foreach (var token in result.Tokens)
                {
                    if (!Uri.TryCreate(token.Value, UriKind.Absolute, out _))
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
