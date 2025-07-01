# WinNotify

A command-line tool for creating Windows toast notifications with customizable text, images, and interactive elements using the Windows App SDK.

## Features

- Custom title and message
- Inline images
- Hero (banner) images
- App logo/icon override
- Multiple notification sounds
- Interactive buttons (up to 5)
- Short or long duration
- Attribution text

## Installation

1. Build the project:
```bash
dotnet build
```

2. Run from the build output or publish for deployment:
```bash
dotnet publish -c Release -r win-x64 --self-contained
```

## Usage

### Basic notification
```bash
WinNotify -m "Hello from WinNotify!"
```

### With custom title
```bash
WinNotify -t "Important Alert" -m "Your task has completed successfully"
```

### With image
```bash
WinNotify -t "Photo" -m "Check out this image" -i "C:\path\to\image.jpg"
```

### With hero image and buttons
```bash
WinNotify -t "Meeting Reminder" -m "Team standup in 5 minutes" --hero "C:\banner.jpg" -b "Join Now,Snooze,Dismiss"
```

### With custom sound
```bash
WinNotify -t "New Email" -m "You have a new message" -s mail
```

### Long duration notification
```bash
WinNotify -t "Download Complete" -m "Your file has been downloaded" -d long
```

## Command-Line Options

- `-t, --title` : Notification title (default: "WinNotify")
- `-m, --message` : Notification message (required)
- `-i, --image` : Path to inline image
- `-l, --logo` : Path to app logo/icon
- `--hero` : Path to hero/banner image
- `-d, --duration` : Duration: short or long (default: short)
- `-s, --sound` : Sound: default, mail, sms, alarm, reminder, or none (default: default)
- `-b, --buttons` : Comma-separated button labels (max 5)
- `-a, --attribution` : Attribution text

## Examples

### System update notification
```bash
WinNotify -t "System Update" -m "Updates are ready to install" -l "C:\Windows\System32\SystemSettingsBroker.exe" -b "Install Now,Restart Later" -s reminder
```

### Social media notification
```bash
WinNotify -t "New Message" -m "John Doe sent you a message" -i "C:\profile.jpg" -a "Facebook Messenger" -s sms
```

### Calendar reminder
```bash
WinNotify -t "Calendar" -m "Meeting with client at 2:00 PM" -d long -s alarm -b "Join Meeting,Snooze 5 min,Dismiss"
```

## Notes

- This tool requires Windows 10/11
- Images should be in common formats (JPG, PNG, GIF)
- Maximum of 5 buttons can be added
- Long duration notifications stay visible for up to 5 minutes

## Troubleshooting

### "Klasse nicht registriert" (Class not registered) Error

This error occurs when the Windows App SDK runtime is not installed. To fix this:

1. **Install Windows App SDK Runtime**: Download and install the latest Windows App SDK runtime from:
   https://aka.ms/windowsappsdk/1.4/latest/windowsappruntimeinstall-x64.exe

2. **Alternative**: Build as self-contained:
   ```bash
   dotnet publish -c Release -r win-x64 --self-contained
   ```

3. **For Development**: Install the Windows App SDK extension for Visual Studio or use the following command:
   ```bash
   dotnet workload install microsoft-windows-sdk-net-workload
   ```