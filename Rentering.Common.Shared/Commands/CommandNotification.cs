using System.Collections.Generic;
using System.Linq;

namespace Rentering.Common.Shared.Commands
{
    public class CommandNotification
    {
        public CommandNotification()
        {
        }

        public CommandNotification(string title, string message)
        {
            Title = title;
            Message = message;
        }

        public string Title { get; set; }
        public string Message { get; set; }
    }

    public static class NotificationExtension
    {
        public static CommandNotification ConvertFluentToCommandNotification(this FluentValidator.Notification fluentNotification) =>
             new CommandNotification(fluentNotification.Property, fluentNotification.Message);

        public static List<CommandNotification> ConvertFluentToCommandNotifications(this IEnumerable<FluentValidator.Notification> fluentNotification)
        {
            var commandNotifications = new List<CommandNotification>();

            fluentNotification.ToList().ForEach(c => commandNotifications.Add(new CommandNotification(c.Property, c.Message)));

            return commandNotifications;
        }
    }

}
