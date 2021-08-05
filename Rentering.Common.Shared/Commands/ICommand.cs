using FluentValidator;

namespace Rentering.Common.Shared.Commands
{
    public abstract class Command : Notifiable
    {
        public Command()
        {
        }

        public virtual void FailFastValidations() { }
    }
}
