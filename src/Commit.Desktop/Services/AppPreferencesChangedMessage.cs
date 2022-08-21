using System;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Commit.Desktop.Services;

public class AppPreferencesChangedMessage : ValueChangedMessage<AppPreferences>
{
    public AppPreferencesChangedMessage(AppPreferences appPreferences) : base(appPreferences)
    {
    }
}
