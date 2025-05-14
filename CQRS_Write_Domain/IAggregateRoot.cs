using CQRS_Write_Domain.Events;

namespace CQRS_Write_Domain
{
    public interface IAggregateRoot<T> : IggregateRoot
    {
        T Id { get; }
    }

    public interface IggregateRoot
    {
        object GetId();
        int Version { get; }

        IEnumerable<IEvent> GetUncommittedChanges();

        void MarkChangesAsCommitted();

        void LoadsFromHistory(IEnumerable<IEvent> history);

        void ApplyChange(IEvent @event, bool isNew = true);
    }
}
