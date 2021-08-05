using System.Collections.Generic;
using System.Linq;

namespace Rentering.Common.Shared.Commands
{
    public static class NotificationExtensions
    {
        public static CommandNotification ConvertFluentToCommandNotification(this FluentValidator.Notification fluentNotification) =>
             new CommandNotification(fluentNotification.Property, fluentNotification.Message);

        public static List<CommandNotification> ConvertCommandNotifications(this IEnumerable<FluentValidator.Notification> fluentNotification)
        {
            var commandNotifications = new List<CommandNotification>();

            fluentNotification.ToList().ForEach(c => commandNotifications.Add(new CommandNotification(c.Property, c.Message)));

            return commandNotifications;
        }
    }
}
