using System.Collections.Generic;

namespace Rentering.Common.Shared.Commands
{
    public interface ICommandResult
    {
        bool Success { get; set; }
        List<CommandNotification> Notifications { get; set; }
        object Data { get; set; }
    }
}
