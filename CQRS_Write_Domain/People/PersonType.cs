namespace CQRS_Write_Domain.People
{
    public struct PersonType
    {        
        public PersonClass Class {  get; private set; }
        public string Nome { get; private set; }
        public int Idade { get; private set; }

        public PersonType(PersonClass personClass, string nome, int idade)
        {
            Class = personClass;
            Nome = nome;
            Idade = idade;
        }
    }
}
