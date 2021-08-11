using FluentValidator;
using System;

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
        public DateTime CreateDate { get; set; }

        public virtual void AssignId(int id)
        {
            if (id <= 0)
                throw new Exception("O id precisa ser maior do que zero.");

            Id = id;
        }
    }
}
