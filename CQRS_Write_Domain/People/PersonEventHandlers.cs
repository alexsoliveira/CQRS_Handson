using CQRS_Read_Application.People;
using CQRS_Write_Domain.Events;

namespace CQRS_Write_Domain.People
{
    public class PersonEventHandlers : IEventHandler<PersonCreatedEvent>, IEventHandler<PersonDeletedEvent>
    {
        private readonly IPersonService personService;

        public PersonEventHandlers(IPersonService personService)
        {
            this.personService = personService;
        }

        public void Handle(PersonCreatedEvent @event)
        {
            CQRS_Read_Infrastructure.Persistence.People.Person person =
                new CQRS_Read_Infrastructure.Persistence.People.Person
                (@event.AggregateId,
                (CQRS_Read_Infrastructure.Persistence.People.PersonClass)(@event.Class),
                @event.Nome,
                @event.Idade);

            this.personService.Insert(person);
        }

        public void Handle(PersonDeletedEvent @event)
        {
            this.personService.Delete(@event.AggregateId);
        }

        public void Handle(PersonRenamedEvent @event)
        {
            var person = this.personService.Find(@event.AggregateId);
            person.Nome = @event.Nome;
            this.personService.Update(person);
        }
    }
}
