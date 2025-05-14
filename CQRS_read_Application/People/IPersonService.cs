using CQRS_read_infrastructure.Persistence.People;

namespace CQRS_read_Application.People
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
