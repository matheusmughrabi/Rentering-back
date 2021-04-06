using FluentValidator;

namespace Rentering.Common.Shared.Notifiables
{
    public class NotifiableRenter
    {
        public NotifiableRenter(Notification notification)
        {
            Notification = notification;
        }

        public long Id { get; set; }
        public Notification Notification { get; set; }
    }
}
