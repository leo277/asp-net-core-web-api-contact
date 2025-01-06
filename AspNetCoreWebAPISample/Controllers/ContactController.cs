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
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ContactController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
