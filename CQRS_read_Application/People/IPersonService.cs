using CQRS_Read_Infrastructure.Persistence.People;

namespace CQRS_Read_Application.People
{
    public interface IPersonService : IApplicationService<Person>
    {
        Person Find(int id);
        IQueryable<Person> GetByName(string name);
        IQueryable<Person> GetAll();
        void Insert(Person person);
        void Update(Person person); 
        void Delete(int id);
    }
}
