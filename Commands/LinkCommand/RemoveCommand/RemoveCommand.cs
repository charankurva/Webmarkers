using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace Webmarkers.Commands
{
    public class RemoveCommand:Command
    {
        #region properties
        private readonly IWebMarker _service;
        private readonly WebMarkerSettings _settings;

        #endregion
        #region constructor
        public RemoveCommand(IWebMarker service, WebMarkerSettings settings) :base("remove","remove the webmarker")
        {
            _service = service;
            _settings = settings;

            this.Options.Add(idOption);
            SetAction((ParseResult) =>
            {
                int idval = ParseResult.GetValue(idOption);
                if (idval != 0)
                {
                    _service.FileReader(_service.InitializeFile(_settings.FilePath));
                    _service.RemoveWebMarker(idval);
                    _service.FileWriter(_service.InitializeFile(_settings.FilePath));
                }
            });
        }
        #endregion
        #region options
        public Option<int> idOption = new Option<int>("--id", "-i") { Required=true,Description="Id of the webmark"};
        
        #endregion
    }
}
