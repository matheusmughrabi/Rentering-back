namespace Rentering.Common.Shared.Entities
{
    public interface IEntity
    {
        int Id { get; }

        void AssignId(int id);
    }
}
