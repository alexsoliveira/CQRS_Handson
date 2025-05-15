using CQRS_Write_Domain;
using CQRS_Write_Domain.Commands;
using CQRS_Write_Domain.Events;

namespace CQRS_Write_Infrastructure.Commands
{
    public class CommandEventRepository : ICommandEventRepository
    {
        private IEventPublisher eventPublisher;
        private Dictionary<object, List<IEvent>> aggregateEventsDictionary = new Dictionary<object, List<IEvent>>();

        public CommandEventRepository(IEventPublisher eventPublisher)
        {
            this.eventPublisher = eventPublisher;
        }

        public T GetByCommandId<T>(object aggregateId) where T : IAggregateRoot
        {
            T aggregate = (T)Activator.CreateInstance(typeof(T));

            List<IEvent> aggregateEvents;
            if (aggregateEventsDictionary.TryGetValue(aggregateId, out aggregateEvents))
            {
                aggregate.LoadsFromHistory(aggregateEvents);

                return aggregate;
            }

            return default;
        }

        public IEnumerable<IEvent> GetEvents(object aggregateId)
        {
            List<IEvent> aggregateEvents;
            if (aggregateEventsDictionary.TryGetValue(aggregateId, out aggregateEvents))
            {
                return aggregateEvents;
            }

            return new List<IEvent>();
        }

        public void Save(IAggregateRoot aggregate)
        {
            List<IEvent> aggregateEvents;
            if (!aggregateEventsDictionary.TryGetValue(aggregate.GetId(), out aggregateEvents))
            {
                aggregateEvents = new List<IEvent>();
                aggregateEventsDictionary.Add(aggregate.GetId(), aggregateEvents);
            }

            // verifica se a versão mais recente do evento corresponde à versão agregada atual

            foreach (var @event in aggregate.GetUncommittedChanges())
            {
                aggregateEvents.Add(@event);
                eventPublisher.Publish(@event);
            }

            aggregate.MarkChangesAsCommitted();
        }
    }
}
