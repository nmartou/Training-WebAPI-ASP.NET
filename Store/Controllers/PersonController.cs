using Microsoft.AspNetCore.Mvc;
using Store.Models;
using Store.Services;

namespace Store.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;
    public PersonController(IPersonService service)
    {
        _personService = service;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Person>> GetPersonById([FromRoute] int id)
    {
        try
        {
            Person? person = await _personService.GetPersonAsync(id);
            if (person is null)
                return NotFound();
            return Ok(person);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Person>>> GetPeople()
    {
        try
        {
            IEnumerable<Person> people = await _personService.GetPeopleAsync();
            return Ok(people);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Person>> CreatePerson([FromBody] Person person)
    {
        try
        {
            await _personService.CreatePersonAsync(person);
            return Created("/api/person/", person);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdatePerson([FromRoute] int id, [FromBody] Person person)
    {
        try
        {
            if(id != person.PersonID)
                return BadRequest("Id from url does not match with the personID");
            await _personService.UpdatePerson(person);
            return NoContent();
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePerson([FromRoute] int id)
    {
        try
        {
            await _personService.DeletePerson(id);
            return NoContent();
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}