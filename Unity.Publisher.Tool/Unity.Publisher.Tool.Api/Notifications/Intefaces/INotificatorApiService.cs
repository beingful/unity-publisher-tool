namespace Unity.Publisher.Tool.Notifications.Intefaces;

public interface INotificatorApiService
{
    Task SendAsync(Notification notification);
}
