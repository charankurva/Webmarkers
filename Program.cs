// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Spectre.Console;
using System.CommandLine;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Xml.Linq;
using Webmarkers;
using Webmarkers.Commands;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();


try
{

    var builder = Host.CreateApplicationBuilder(args);
   
    builder.Services.AddSerilog((services, logconfiguration) =>
    {
        logconfiguration.ReadFrom.Configuration(builder.Configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext();
    }
        );
    builder.Services.AddSingleton<IWebMarker, WebMarkerServices>();
    builder.Services.AddSingleton<WebMarkerSettings>();

    using IHost host = builder.Build();
    IWebMarker _webmark = host.Services.GetRequiredService<IWebMarker>();
    WebMarkerSettings _settings= host.Services.GetRequiredService<WebMarkerSettings>();
    host.RunAsync();



Console.WriteLine("Welcome to Webmarker  o(*^＠^*)o");
    #region manualfilecreation
    //settting up default storage path for json file
    //string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebMarker.json");
    //FileInfo JsonFile = new FileInfo(filePath);
    //reading the filecontent into webmarkobject
    //if (File.Exists(JsonFile.FullName))
    //{
    //    _webmark.FileReader(JsonFile);
    //}
    #endregion
    var rootcommand = new RootCommand("a tool to add your web markings");

    _webmark.FileReader(_webmark.InitializeFile(_settings.FilePath));
    rootcommand.Subcommands.Add(new LinkCommand(_webmark,_settings));
  rootcommand.Subcommands.Add(new InterractiveCommand(_settings,_webmark));
  ParseResult parseResult = rootcommand.Parse(args);
  
 
    #region oldcode
    //    var nameoption = new Option<string[]>("--name", "-n") { Description = "takes the website name", Required = true,AllowMultipleArgumentsPerToken=true };
    //var urloption = new Option<string[]>("--url", "-u") { Description = "takes the website url", Required = true, AllowMultipleArgumentsPerToken = true };
    //var categoryoption = new Option<string[]>("--category", "-c") { Description = "takes the website catogory", Required = false, AllowMultipleArgumentsPerToken = true };
    //categoryoption.AcceptOnlyFromAmong("movie", "learning", "anime");
    //var idOption = new Option<int>("--id", "-i");
    //var interactiveCommand = new Command("interactive", "Manage bookmarks interactively")
    //{
    //};
    //rootcommand.Subcommands.Add(interactiveCommand);
    //var fileinputoption = new Option<FileInfo>("--file", "-f") { DefaultValueFactory=(_)=> { return file; } };

    //urloption.Validators.Add(result =>
    //{
    //    foreach (var token in result.Tokens)
    //    {
    //        if (string.IsNullOrWhiteSpace(token.Value))
    //        {
    //            result.AddError("url cannot be empty");
    //            break;
    //        }
    //        else if (!Uri.TryCreate(token.Value, UriKind.Absolute, out _)) {
    //            result.AddError($"Invalid url:{token.Value}");
    //            break;                                

    //        }
    //    }
    //});


    //var linkcommand = new Command("link", "used to perform operatios on links");
    //var addcommand = new Command("add", "used to add the web mark") {
    //nameoption,
    //urloption,
    //categoryoption,

    //};
    //var removeCommand = new Command("remove", "remove the webmark") {
    //    idOption

    //};
    //var updateCommand = new Command("update", "update the url") { idOption,urloption };

    //var listcommand = new Command("list", "used to list all the bookmarks") { 
    //    categoryoption
    //};
    //var importcommand = new Command("import", "importing the bookmarks") {

    //};


    //rootcommand.Subcommands.Add(linkcommand);
    //linkcommand.Subcommands.Add(addcommand);
    //linkcommand.Subcommands.Add(importcommand);
    //linkcommand.Subcommands.Add(listcommand);
    //linkcommand.Subcommands.Add(removeCommand);
    //linkcommand.Subcommands.Add(updateCommand);
    //ParseResult parseResult = rootcommand.Parse(args);


    //    listcommand.SetAction(ListWebMarks);


    //    void ListWebMarks(ParseResult ParseResult)
    //    {
    //        string[] category = parseResult.GetValue(categoryoption);
    //        _webmark.ListWebMarker(category);

    //    }
    //    //rootcommand.SetAction(OnInterractiveCommand);
    //    interactiveCommand.SetAction(OnInterractiveCommand);
    //void OnInterractiveCommand(ParseResult parseresult)
    //{
    //        bool changed = false;
    //    var appName = new FigletText("WEBMARKER")
    //    {
    //        Color = Color.Orange3,
    //        Justification = Justify.Center
    //    };

    //    var version = new Text("Version 1.0.0", new Style(Color.Grey))
    //    {
    //        Justification = Justify.Center
    //    };

    //    AnsiConsole.Write(appName);
    //    AnsiConsole.Write(version);
    //        bool IsRunnning = true;
    //        while (IsRunnning)
    //        {


    //            var menu = AnsiConsole.Prompt(
    //                new SelectionPrompt<string>()
    //                .Title("What you wanna do?")
    //                .AddChoices("list", "add", "remove", "update","exit"));
    //            AnsiConsole.MarkupLine($"you selected [green]{menu}[/]");

    //            switch (menu)
    //            {
    //                case "list":
    //                    {
    //                        if (changed)
    //                        {
    //                            _webmark.FileWriter(JsonFile);
    //                        }
    //                        ListWebMarks(parseresult);
    //                        changed = false;
    //                        break;
    //                    }
    //                case "add":
    //                    {
    //                        var web_name = AnsiConsole.Ask<string>("[green]Enter your website name[/]:");
    //                        var web_url = AnsiConsole.Ask<string>("[green]Enter your website url[/]:");
    //                        var category = AnsiConsole.Prompt<string>(new SelectionPrompt<string>()
    //                            .Title("[yellow]choose a category:[/]")
    //                            .AddChoices("anime", "movie", "learning"));

    //                        _webmark.AddWebMarker(web_name, web_url, category);

    //                        AnsiConsole.MarkupLine(web_name);
    //                        changed = true;
    //                        break;
    //                    }
    //                case "update":
    //                    {
    //                        var id = AnsiConsole.Ask<int>("[green]Enter your website id[/]:");
    //                        var web_url = AnsiConsole.Ask<string[]>("[green]Enter your new website url[/]:");
    //                        _webmark.UpdateWebMarker(id, web_url);
    //                        changed = true;
    //                        break;
    //                    }
    //                case "remove":
    //                    {
    //                        var id = AnsiConsole.Ask<int>("[green]Enter your website id[/]:");
    //                        _webmark.RemoveWebMarker(id);
    //                        changed = true;
    //                        break;
    //                    }
    //                default:IsRunnning = false;break;
    //            }

    //        }
    //        }


    //    addcommand.SetAction((parseResult) => {
    //    string[] name = parseResult.GetValue(nameoption);
    //    string[] url = parseResult.GetValue(urloption);
    //    string[] category = parseResult.GetValue(categoryoption);

    //    _webmark.FileReader(JsonFile);
    //    _webmark.AddWebMarker(name, url, category);
    //_webmark.FileWriter(JsonFile);

    //});

    //removeCommand.SetAction((ParseResult) =>
    //{
    //    int idval = ParseResult.GetValue(idOption);
    //    if (idval != 0)
    //    {
    //        _webmark.FileReader(JsonFile);
    //        _webmark.RemoveWebMarker(idval);
    //        _webmark.FileWriter(JsonFile);
    //    }
    //});

    //updateCommand.SetAction((ParseResult) => {
    //    int idval = ParseResult.GetValue(idOption);
    //    string[] url=parseResult.GetValue(urloption);
    //    if (idval != 0)
    //    {
    //        _webmark.FileReader(JsonFile);
    //        _webmark.UpdateWebMarker(idval, url);
    //        _webmark.FileWriter(JsonFile);
    //    }

    //});

    //importcommand.SetAction((parseResult) => { });
    #endregion

    parseResult.Invoke();

}
catch (Exception ex)
{
    Log.Fatal(ex, "application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
