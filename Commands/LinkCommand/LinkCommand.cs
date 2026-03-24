using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;


namespace Webmarkers.Commands
{
    public class LinkCommand:Command
    {
        private readonly IWebMarker _service;
        private readonly WebMarkerSettings _settings;
        #region constructor
        public LinkCommand(IWebMarker service):base("link", "used to perform operatios on links") { 
            _service = service;
            Subcommands.Add(new AddCommand(_service, _settings));
            Subcommands.Add(new ListCommand(_service));
            Subcommands.Add(new UpdateCommand(_service, _settings));
            Subcommands.Add(new RemoveCommand(_service,_settings));


        }
        #endregion

        #region addsubcommand

        #endregion


    }
}
