using Microsoft.AspNetCore.Mvc.RazorPages;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text;

namespace Webmarkers.Commands
{
    public class InterractiveCommand:Command
    {
        #region property
        private readonly WebMarkerSettings _settings;
        private readonly IWebMarker _service;
        #endregion
        public InterractiveCommand(WebMarkerSettings settings, IWebMarker service) : base("interractive","Used to get interractive ui")
        {
            _settings = settings;
            _service = service;
            SetAction(OnInterractiveCommand);
        }

        private void OnInterractiveCommand(ParseResult parseresult)
        {
            bool changed = false;
            var appName = new FigletText("WEBMARKER")
            {
                Color = Color.Orange3,
                Justification = Justify.Center
            };

            var version = new Text("Version 1.0.0", new Style(Color.Grey))
            {
                Justification = Justify.Center
            };

            AnsiConsole.Write(appName);
            AnsiConsole.Write(version);
            bool IsRunnning = true;
            
            while (IsRunnning)
            {
                var menu = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("What you wanna do?")
                    .AddChoices("list", "add", "remove", "update", "exit"));
                AnsiConsole.MarkupLine($"you selected [green]{menu}[/]");

                switch (menu)
                {
                    case "list":
                        {
                            if (changed)
                            {
                                _service.FileWriter(_service.InitializeFile(_settings.FilePath));
                            }
                            
                            _service.ListWebMarker(null);
                            changed = false;
                            break;
                        }
                    case "add":
                        {
                            var web_name = AnsiConsole.Ask<string>("[green]Enter your website name[/]:");
                            var web_url = AnsiConsole.Ask<string>("[green]Enter your website url[/]:");
                            var category = AnsiConsole.Prompt<string>(new SelectionPrompt<string>()
                                .Title("[yellow]choose a category:[/]")
                                .AddChoices("anime", "movie", "learning"));

                            _service.AddWebMarker(web_name, web_url, category);

                            AnsiConsole.MarkupLine(web_name);
                            changed = true;
                            break;
                        }
                    case "update":
                        {
                            var id = AnsiConsole.Ask<int>("[green]Enter your website id[/]:");
                            var web_url = AnsiConsole.Ask<string>("[green]Enter your new website url[/]:");
                            _service.UpdateWebMarker(id, web_url);
                            changed = true;
                            break;
                        }
                    case "remove":
                        {
                            var id = AnsiConsole.Ask<int>("[green]Enter your website id[/]:");
                            _service.RemoveWebMarker(id);
                            changed = true;
                            break;
                        }
                    default: IsRunnning = false; break;
                }

            }
        }
    }
}
