namespace Rentering.Common.Shared.Entities
{
    public interface IEntity
    {
        int Id { get; set; }

        void AssignId(int id);
    }
}
