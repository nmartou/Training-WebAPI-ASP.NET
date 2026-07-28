
using Microsoft.AspNetCore.Http.HttpResults;
using Store.Models;
using Store.Repositories;

namespace Store.Services;

public class PersonService : IPersonService
{
    private readonly IRepository<Person> _personRepository;

    public PersonService(IRepository<Person> repository)
    {
        _personRepository = repository;
    }
    public async Task<Person> CreatePersonAsync(Person person)
    {
        if(person.FirstName is null)
            throw new ArgumentException("FirstName is empty");
        else if(person.LastName is null)
            throw new ArgumentException("LastName is empty");
        else if(person.BirthDate > new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day))
            throw new ArgumentOutOfRangeException("BirthDate is empty");

        await _personRepository.AddAsync(person);
        await _personRepository.SaveChangesAsync();
        return person;
    }

    public async Task DeletePerson(int id)
    {
        Person? person = await _personRepository.GetByIdAsync(id);
        if(person is null)
            throw new ArgumentException($"Person with id {id} does not exist");
        
        _personRepository.Delete(person);
        await _personRepository.SaveChangesAsync();
    }

    public async Task<IEnumerable<Person>> GetPeopleAsync()
    {
        return await _personRepository.GetAllAsync();
    }

    public async Task<Person?> GetPersonAsync(int id)
    {
        return await _personRepository.GetByIdAsync(id);
    }

    public async Task UpdatePerson(Person person)
    {
        Person? currPerson = await _personRepository.GetByIdAsync(person.PersonID);
        if(currPerson is null)
            throw new ArgumentException($"Person id {person.PersonID} does not exists");
        else if(person.BirthDate > new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day))
            throw new ArgumentOutOfRangeException("BirthDate is empty");
        
        currPerson.FirstName = person.FirstName;
        currPerson.LastName = person.LastName;
        currPerson.BirthDate = person.BirthDate;
        await _personRepository.SaveChangesAsync();
    }
}