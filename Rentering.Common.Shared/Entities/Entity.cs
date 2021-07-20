using FluentValidator;

namespace Rentering.Common.Shared.Entities
{
    public abstract class Entity : Notifiable
    {
        public Entity(int? id = null)
        {
            if (id != null)
                AssignId((int)id);
        }

        public int Id { get; private set; }

        public virtual void AssignId(int id)
        {
            if (id <= 0)
                AddNotification("Id", "O id precisa ser maior do que zero");

            Id = id;
        }
    }
}
