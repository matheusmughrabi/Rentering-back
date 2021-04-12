using FluentValidator;

namespace Rentering.Common.Shared.Entities
{
    public abstract class Entity : Notifiable
    {
        public int Id { get; private set; }

        protected void AssignId(int id)
        {
            if (id <= 0)
                AddNotification("Id", "Id must be greater than zero");

            Id = id;
        }
    }
}
