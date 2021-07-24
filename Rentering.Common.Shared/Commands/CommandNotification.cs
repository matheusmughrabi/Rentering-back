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
}
