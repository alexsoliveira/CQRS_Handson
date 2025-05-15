using CQRS_Write_Domain.Events;

namespace CQRS_Write_Domain.People
{
    public class PersonCreatedEvent : Event
    {
        public PersonClass Class { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }

        public PersonCreatedEvent(int aggregateId, PersonClass personClass, string nome, int idade)
        {
            this.AggregateId = aggregateId;
            Class = personClass;
            Nome = nome;
            Idade = idade;
        }
    }
}
