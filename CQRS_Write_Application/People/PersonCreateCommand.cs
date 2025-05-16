using CQRS_Write_Domain.Commands;
using CQRS_Write_Domain.People;

namespace CQRS_Write_Application.People
{
    public class PersonCreateCommand : Command
    {        
        public PersonClass Class { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }

        public PersonCreateCommand(PersonClass personClass, string nome, int idade)
        {
            Class = personClass;
            Nome = nome;
            Idade = idade;
        }
    }
}
