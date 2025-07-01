using CommandLine;

namespace WinNotify;

public class Options
{
    [Option('t', "title", Required = false, HelpText = "Notification title", Default = "WinNotify")]
    public string Title { get; set; } = "WinNotify";

    [Option('m', "message", Required = true, HelpText = "Notification message")]
    public string Message { get; set; } = string.Empty;

    [Option('i', "image", Required = false, HelpText = "Path to image file")]
    public string? ImagePath { get; set; }

    [Option('l', "logo", Required = false, HelpText = "Path to app logo/icon")]
    public string? AppLogoPath { get; set; }

    [Option('d', "duration", Required = false, HelpText = "Duration: short or long", Default = "short")]
    public string Duration { get; set; } = "short";

    [Option('s', "sound", Required = false, HelpText = "Notification sound: default, mail, sms, alarm, reminder, or none", Default = "default")]
    public string Sound { get; set; } = "default";

    [Option('b', "buttons", Required = false, HelpText = "Comma-separated button labels (max 5)")]
    public string? Buttons { get; set; }

    [Option('a', "attribution", Required = false, HelpText = "Attribution text")]
    public string? Attribution { get; set; }

    [Option("hero", Required = false, HelpText = "Path to hero image (large banner image)")]
    public string? HeroImagePath { get; set; }
}