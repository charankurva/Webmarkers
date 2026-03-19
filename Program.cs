// See https://aka.ms/new-console-template for more information
using System.CommandLine;
using System.Reflection.Metadata;
using System.Text.Json;
using Webmarkers;

WebMarkerServices _webmark = new WebMarkerServices();
Console.WriteLine("Welcome to Webmarker  o(*^＠^*)o");
//settting up default storage path for json file
string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebMarker.json");
FileInfo JsonFile = new FileInfo(filePath);
var rootcommand = new RootCommand("a tool to add your web markings");

var nameoption = new Option<string[]>("--name", "-n") { Description = "takes the website name", Required = true,AllowMultipleArgumentsPerToken=true };
var urloption = new Option<string[]>("--url", "-u") { Description = "takes the website url", Required = true, AllowMultipleArgumentsPerToken = true };
var categoryoption = new Option<string[]>("--category", "-c") { Description = "takes the website catogory", Required = false, AllowMultipleArgumentsPerToken = true };
categoryoption.AcceptOnlyFromAmong("movie", "learning", "anime");
var idOption = new Option<int>("--id", "-i");
//var fileinputoption = new Option<FileInfo>("--file", "-f") { DefaultValueFactory=(_)=> { return file; } };

urloption.Validators.Add(result =>
{
    foreach (var token in result.Tokens)
    {
        if (string.IsNullOrWhiteSpace(token.Value))
        {
            result.AddError("url cannot be empty");
            break;
        }
        else if (!Uri.TryCreate(token.Value, UriKind.Absolute, out _)) {
            result.AddError($"Invalid url:{token.Value}");
            break;                                
        
        }
    }
});


var linkcommand = new Command("link", "used to perform operatios on links");
var addcommand = new Command("add", "used to add the web mark") {
nameoption,
urloption,
categoryoption,

};
var removeCommand = new Command("remove", "remove the webmark") {
    idOption

};
var updateCommand = new Command("update", "update the url") { idOption,urloption };

var listcommand = new Command("list", "used to list all the bookmarks") { 
    categoryoption
};
var importcommand = new Command("import", "importing the bookmarks") {

};


rootcommand.Subcommands.Add(linkcommand);
linkcommand.Subcommands.Add(addcommand);
linkcommand.Subcommands.Add(importcommand);
linkcommand.Subcommands.Add(listcommand);
linkcommand.Subcommands.Add(removeCommand);
linkcommand.Subcommands.Add(updateCommand);
ParseResult parseResult = rootcommand.Parse(args);

listcommand.SetAction((ParseResult) => {
    string[] category =parseResult.GetValue(categoryoption);
    if (File.Exists(JsonFile.FullName))
    {
        _webmark.FileReader(JsonFile);
    }
    _webmark.ListWebMarker(category); 

});


addcommand.SetAction((parseResult) => {
    string[] name = parseResult.GetValue(nameoption);
    string[] url = parseResult.GetValue(urloption);
    string[] category = parseResult.GetValue(categoryoption);

    _webmark.FileReader(JsonFile);
    _webmark.AddWebMarker(name, url, category);
_webmark.FileWriter(JsonFile);

});
removeCommand.SetAction((ParseResult) =>
{
    int idval = ParseResult.GetValue(idOption);
    if (idval != 0)
    {
        _webmark.FileReader(JsonFile);
        _webmark.RemoveWebMarker(idval);
        _webmark.FileWriter(JsonFile);
    }
});

updateCommand.SetAction((ParseResult) => {
    int idval = ParseResult.GetValue(idOption);
    string[] url=parseResult.GetValue(urloption);
    if (idval != 0)
    {
        _webmark.FileReader(JsonFile);
        _webmark.UpdateWebMarker(idval, url);
        _webmark.FileWriter(JsonFile);
    }

});

parseResult.Invoke();
    

