// See https://aka.ms/new-console-template for more information
using System.CommandLine;
using Webmarkers;
WebMarkerServices webmark = new WebMarkerServices();
Console.WriteLine("Welcome to Webmarker  o(*^＠^*)o");

var rootcommand = new RootCommand("a tool to add your web markings");

var nameoption = new Option<string[]>("--name", "-n") { Description = "takes the website name", Required = true,AllowMultipleArgumentsPerToken=true };
var urloption = new Option<string[]>("--url", "-u") { Description = "takes the website url", Required = true, AllowMultipleArgumentsPerToken = true };
var categoryoption = new Option<string[]>("--category", "-c") { Description = "takes the website catogory", Required = true, AllowMultipleArgumentsPerToken = true };
categoryoption.AcceptOnlyFromAmong("movie websites", "learning websites", "anime websites");


var linkcommand = new Command("link", "used to perform operatios on links");
var addcommand = new Command("add", "used to add the web mark") {
nameoption,
urloption,
categoryoption
};

rootcommand.Subcommands.Add(linkcommand);
linkcommand.Subcommands.Add(addcommand);

ParseResult parseResult = rootcommand.Parse(args);

addcommand.SetAction((parseResult) => {
    string[] name = parseResult.GetValue(nameoption);
    string[] url = parseResult.GetValue(urloption);
    string[] category = parseResult.GetValue(categoryoption);
    webmark.AddWebMarker(name,url, category);

    var Listofwebmarks=webmark.ListWebMarker();
    foreach (var webmark in Listofwebmarks)
    {
        Console.WriteLine($"{webmark.WebName} | {webmark.WebUrl} |{webmark.Category}");
    }

});

parseResult.Invoke();
    

