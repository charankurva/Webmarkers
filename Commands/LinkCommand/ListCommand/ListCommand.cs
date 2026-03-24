using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace Webmarkers.Commands
{
    public class ListCommand:Command
    {
        #region properties
        private readonly IWebMarker _service;
        #endregion

        #region constructor
        public ListCommand(IWebMarker service):base("list","list of all the webmarkers")
        {
            _service = service;
            this.Options.Add(categoryoption);
            categoryoption.AcceptOnlyFromAmong("movie", "learning", "anime");
            SetAction((parseresult) => {

                string category = parseresult.GetValue(categoryoption);
                _service.ListWebMarker(category);

            });
        }
        #endregion

        #region options
        public Option<string> categoryoption = new Option<string>("--category", "-c") { Description = "takes the website catogory", Required = false };

        #endregion

    }
}
