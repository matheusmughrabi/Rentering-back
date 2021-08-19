using FluentValidator;

namespace Rentering.Common.Shared.Commands
{
    public abstract class Command : Notifiable
    {
        public virtual void FailFastValidations() { }
    }
}
