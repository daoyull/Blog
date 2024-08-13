using Avalonia.Controls.Notifications;
using Avalonia.Threading;
using Common.Avalonia.Service;

namespace BlogBackManager.Service.Impl;

public class GlobalNotify : IGlobalNotify
{
    public WindowNotificationManager? NotificationManager { get; set; }

    public void Message(Notification notification)
    {
        if (NotificationManager != null)
        {
            Dispatcher.UIThread.Invoke(() => { NotificationManager.Show(notification); });
        }
    }
}