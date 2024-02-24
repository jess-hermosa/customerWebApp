using CustomerAPI.Models;
using CustomerAPI.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public async Task<List<Customer>> Get()
        {
            return await _customerService.GetAll();
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public async Task<Customer> Get(int id)
        {
            return await _customerService.Get(id);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public void Post([FromBody] Customer customer)
        {
            _customerService.AddCustomer(customer);
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Customer customer)
        {
            _customerService.UpdateCustomer(customer);
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _customerService.DeleteCustomer(id);
        }
    }
}
