using CQRS_Write_Domain.Events;

namespace CQRS_Write_Domain.People
{
    public class PersonDeletedEvent : Event
    {
        public PersonDeletedEvent(int aggregateId) : base()
        {
            this.AggregateId = aggregateId;
        }
    }
}
