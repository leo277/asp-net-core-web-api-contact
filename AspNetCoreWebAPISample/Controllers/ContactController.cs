//using System.Web.Http;
using AspNetCoreWebAPISample.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNetCoreWebAPISample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        // create dummy list of contacts
        Contact[] contacts = new Contact[]
        {
            new Contact() { Id = 0, FirstName = "Peter", LastName = "Parker" },
            new Contact() { Id = 1, FirstName = "Bruce", LastName = "Banner" },
            new Contact() { Id = 2, FirstName = "Tony", LastName = "Stark" }
        };

        // GET: api/<ContactController>
        [HttpGet]
        public IEnumerable<Contact> Get()
        {
            return contacts;
        }

        // GET api/<ContactController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Contact contact = contacts.FirstOrDefault(c => c.Id == id);

            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        //GET: api/Contact/search?name=John
        /* search is case insensitive */
        [HttpGet("search")]
        public IActionResult GetContactsByName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return BadRequest("Nmae parameter is required");
            
            Contact[] contactArray = contacts.Where<Contact>(c => c.FirstName.Contains(name, StringComparison.OrdinalIgnoreCase) || c.LastName.Contains(name, StringComparison.OrdinalIgnoreCase)).ToArray<Contact>();
            
            return Ok(contactArray);
        }

        // POST api/<ContactController>
        [HttpPost]
        public IEnumerable<Contact> Post([FromBody] Contact newContact)
        {
            List<Contact> contactList = contacts.ToList<Contact>();

            newContact.Id = contactList.Count;
            contactList.Add(newContact);
            contacts = contactList.ToArray();

            return contacts;
        }

        // PUT api/<ContactController>/5
        [HttpPut("{id}")]
        public IEnumerable<Contact> Put(int id, [FromBody] Contact changedContact)
        {
            Contact? contact = contacts.FirstOrDefault<Contact>(c => c.Id == id);
            if (changedContact != null && contact != null)
            {
                if(changedContact.FirstName != null)
                    contact.FirstName = changedContact.FirstName;
                if (changedContact.LastName != null)
                    contact.LastName = changedContact.LastName;
            }

            return contacts;
        }

        // DELETE api/<ContactController>/5
        [HttpDelete("{id}")]
        public IEnumerable<Contact> Delete(int id)
        {
            if (id >= 0) 
                contacts = contacts.Where(c => c.Id != id).ToArray<Contact>();
            return contacts;
        }
    }
}
