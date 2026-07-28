using Store.Models;

namespace Store.Services;
public interface IPersonService
{
    Task<Person?> GetPersonAsync(int id);
    Task<IEnumerable<Person>> GetPeopleAsync();
    Task<Person> CreatePersonAsync(Person person);
    Task UpdatePerson(Person person);
    Task DeletePerson(int id);
}