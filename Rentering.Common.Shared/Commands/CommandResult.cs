using System.Collections.Generic;

namespace Rentering.Common.Shared.Commands
{
    public class CommandResult : ICommandResult
    {
        public CommandResult()
        {
            Notifications = new List<CommandNotification>();
        }

        public CommandResult(bool success, string message, List<CommandNotification> notifications, object data) 
        {
            Success = success;
            Message = message;
            Notifications = notifications;
            Data = data;

            if (Notifications == null)
                Notifications = new List<CommandNotification>();
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public List<CommandNotification> Notifications { get; set; }
        public object Data { get; set; }

        public void AddNotification(string message, string title)
        {
            var notification = new CommandNotification(title, message);
            Notifications.Add(notification);
        }
    }
}
