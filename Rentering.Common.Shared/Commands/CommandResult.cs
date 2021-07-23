using System.Collections.Generic;

namespace Rentering.Common.Shared.Commands
{
    public class CommandResult : ICommandResult
    {
        public CommandResult()
        {
        }

        public CommandResult(bool success, string message, List<CommandNotification> notifications, object data)
        {
            Success = success;
            Message = message;
            Notifications = notifications;
            Data = data;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public List<CommandNotification> Notifications { get; set; }
        public object Data { get; set; }
    }
}
