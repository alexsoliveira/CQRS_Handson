using CQRS_Read_Application.People;
using CQRS_Write_Domain.Commands;
using CQRS_Write_Domain.People;

namespace CQRS_Write_Application.People
{
    public class PersonCommandHandlers 
        : ICommandHandler<PersonCreateCommand>
        , ICommandHandler<PersonDeleteCommand>
        , ICommandHandler
    {
        private readonly ICommandEventRepository eventRepository;
        private readonly IPersonService personService;

        public PersonCommandHandlers(
            IPersonService personService,
            ICommandEventRepository eventRepository)
        {
            this.eventRepository = eventRepository;
            this.personService = personService;
        }

        public void Handler(PersonCreateCommand command)
        {
            Person person = new Person(this.personService.GetAll().Count() + 1,
                command.Class, command.Nome, command.Idade);
            this.eventRepository.Save(person);
        }

        public void Handler(PersonDeleteCommand command)
        {
            Person person = this.eventRepository.GetByCommandId<Person>(command.Id);
            person.Delete();
            this.eventRepository.Save(person);
        }

        public void Handler(PersonRenamedCommand command)
        {
            Person person = this.eventRepository.GetByCommandId<Person>(command.Id);
            person.Rename(command.Nome);
            this.eventRepository.Save(person);
        }
    }
}
