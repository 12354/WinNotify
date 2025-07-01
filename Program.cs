using CommandLine;
using Microsoft.Windows.AppNotifications;
using Microsoft.Windows.AppNotifications.Builder;
using WinNotify;

class Program
{
    private static bool _isRegistered = false;
    
    static int Main(string[] args)
    {
        try
        {
            // Initialize notification manager
            var notificationManager = AppNotificationManager.Default;
            
            // Subscribe to the NotificationInvoked event before calling Register()
            notificationManager.NotificationInvoked += OnNotificationInvoked;
            
            // Register for unpackaged apps (no parameters needed)
            notificationManager.Register();
            _isRegistered = true;
            
            var result = Parser.Default.ParseArguments<Options>(args)
                .MapResult(
                    (Options opts) => RunNotification(opts),
                    errs => 1);
            
            if (_isRegistered)
            {
                AppNotificationManager.Default.Unregister();
            }
            
            return result;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Initialization error: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.Error.WriteLine($"Inner exception: {ex.InnerException.Message}");
            }
            return 1;
        }
    }


    static void OnNotificationInvoked(AppNotificationManager sender, AppNotificationActivatedEventArgs args)
    {
        Console.WriteLine("Notification activated!");
        
        foreach (var arg in args.Arguments)
        {
            Console.WriteLine($"{arg.Key}: {arg.Value}");
        }
    }

    static int RunNotification(Options opts)
    {
        try
        {
            var builder = new AppNotificationBuilder()
                .AddText(opts.Title)
                .AddText(opts.Message);

            if (!string.IsNullOrEmpty(opts.Attribution))
            {
                builder.SetAttributionText(opts.Attribution);
            }

            if (!string.IsNullOrEmpty(opts.ImagePath) && File.Exists(opts.ImagePath))
            {
                builder.SetInlineImage(new Uri(Path.GetFullPath(opts.ImagePath)));
            }

            if (!string.IsNullOrEmpty(opts.HeroImagePath) && File.Exists(opts.HeroImagePath))
            {
                builder.SetHeroImage(new Uri(Path.GetFullPath(opts.HeroImagePath)));
            }

            if (!string.IsNullOrEmpty(opts.AppLogoPath) && File.Exists(opts.AppLogoPath))
            {
                builder.SetAppLogoOverride(new Uri(Path.GetFullPath(opts.AppLogoPath)));
            }

            if (!string.IsNullOrEmpty(opts.Buttons))
            {
                var buttons = opts.Buttons.Split(',', StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < Math.Min(buttons.Length, 5); i++)
                {
                    builder.AddButton(new AppNotificationButton(buttons[i].Trim())
                        .AddArgument("action", $"button{i}"));
                }
            }

            var audioUri = opts.Sound.ToLower() switch
            {
                "mail" => new Uri("ms-winsoundevent:Notification.Mail"),
                "sms" => new Uri("ms-winsoundevent:Notification.SMS"),
                "alarm" => new Uri("ms-winsoundevent:Notification.Looping.Alarm"),
                "reminder" => new Uri("ms-winsoundevent:Notification.Reminder"),
                "none" => null,
                _ => new Uri("ms-winsoundevent:Notification.Default")
            };

            if (audioUri != null)
            {
                builder.SetAudioUri(audioUri);
            }
            else
            {
                builder.MuteAudio();
            }

            if (opts.Duration.ToLower() == "long")
            {
                builder.SetDuration(AppNotificationDuration.Long);
            }

            var notification = builder.BuildNotification();
            AppNotificationManager.Default.Show(notification);

            Console.WriteLine("Notification sent successfully!");
            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
            return 1;
        }
    }
}