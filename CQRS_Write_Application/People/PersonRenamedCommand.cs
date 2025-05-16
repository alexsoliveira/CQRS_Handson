using CQRS_Write_Domain.Commands;

namespace CQRS_Write_Application.People
{
    public class PersonRenamedCommand : Command
    {        
        public int Id { get; set; }
        public string Nome { get; set; }

        public PersonRenamedCommand(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
}
