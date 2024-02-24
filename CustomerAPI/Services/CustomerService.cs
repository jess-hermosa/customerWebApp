using CustomerAPI.Models;

namespace CustomerAPI.Services
{
    public interface ICustomerService
    {
        public Task<List<Customer>> GetPaginated(int page, int pageSize);
        public Task<Customer> Get(Guid id);
        public void AddCustomer(Customer customer);
        public void UpdateCustomer(Customer customer);
        public void DeleteCustomer(Guid id);
        public Task<int> TotalCustomers { get; }
    }

    internal class CustomerService : ICustomerService
    {

        public Task<List<Customer>> GetPaginated(int page, int pageSize)
        {
            var paginatedCustomer = Customers
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Task.FromResult(paginatedCustomer);
        }

        public Task<Customer> Get(Guid id)
        {
            var existingCustomer = Customers.FirstOrDefault(i => i.Id == id);
            if (existingCustomer != null)
            {
                return Task.FromResult(existingCustomer);
            }
            else
            {
                throw new KeyNotFoundException("Customer not found");
            }
        }

        public void AddCustomer(Customer customer)
        {
            try
            {
                customer.Id = Guid.NewGuid();
                Customers.Add(customer);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            var existingCustomerIndex = Customers.FindIndex(i => i.Id == customer.Id);
            if (existingCustomerIndex != -1)
            {
                Customers[existingCustomerIndex] = customer;
            }
            else
            {
                throw new KeyNotFoundException("Customer not found");
            }
        }

        public void DeleteCustomer(Guid id)
        {
            var customerToRemove = Customers.FirstOrDefault(customer => customer.Id == id);
            if (customerToRemove != null)
            {
                Customers.Remove(customerToRemove);
            }
            else
            {
                throw new KeyNotFoundException("Customer not found");
            }
        }

        public Task<int> TotalCustomers => Task.FromResult(Customers.Count());

        private static List<Customer> Customers = new List<Customer>
        {
            new Customer
            {
                Id = Guid.NewGuid(),
                Firstname = "Merritt",
                Lastname = "Alvarez",
                Address = "632-5477 Lectus Street",
                City = "San Andrés",
                Country = "Costa Ric",
                Email = "mollis.dui@icloud.net",
                Phone = "1-516-116-1363",
                PostalCode = "86-43",
                Region = "Luik",
            },
            new Customer
            {
                Id = Guid.NewGuid(),
                Firstname = "Phoebe",
                Lastname = "Franklin",
                Address = "749-2192 Vestibulum, St.",
                City = "Delicias",
                Country = "Chile",
                Email = "adipiscing.elit@yahoo.edu",
                Phone = "(247) 477-7627",
                PostalCode = "413771",
                Region = "Southwestern Tagalog Region",
            },
            new Customer
            {
                Id = Guid.NewGuid(),
                Firstname = "Ella",
                Lastname = "Short",
                Address = "Ap #538-2254 Vitae, Street",
                City = "Tacurong",
                Country = "Singapore",
                Email = "ac@yahoo.edu",
                Phone = "(216) 722-5354",
                PostalCode = "688911",
                Region = "Valparaíso",
            },
            new Customer
            {
                Id = Guid.NewGuid(),
                Firstname = "Caleb",
                Lastname = "Vincent",
                Address = "Ap #245-4260 Nec, Rd.",
                City = "Lincoln",
                Country = "Austria",
                Email = "phasellus.dapibus@hotmail.couk",
                Phone = "1-263-507-0768",
                PostalCode = "3541-8295",
                Region = "Yukon",
            },
            new Customer
            {
                Id = Guid.NewGuid(),
                Firstname = "Kiayada",
                Lastname = "Summers",
                Address = "504-5663 Urna. St.",
                City = "Trujillo",
                Country = "Costa Rica",
                Email = "magna@hotmail.edu",
                Phone = "1-383-416-2391",
                PostalCode = "959863",
                Region = "Centre",
            },
            new Customer
            {
                Id = Guid.NewGuid(),
                Firstname = "Nathaniel",
                Lastname = "Bush",
                Address = "P.O. Box 341, 6120 Egestas. Road",
                City = "Jayapura",
                Country = "Nigeria",
                Email = "urna.vivamus@icloud.couk",
                Phone = "1-184-574-5217",
                PostalCode = "944538",
                Region = "Bayern",
            },
            new Customer
            {
                Id = Guid.NewGuid(),
                Firstname = "Anne",
                Lastname = "Kelly",
                Address = "P.O. Box 237, 3297 Nulla Avenue",
                City = "Gunsan",
                Country = "Ireland",
                Email = "erat.vivamus@protonmail.couk",
                Phone = "(149) 719-2939",
                PostalCode = "4295 GI",
                Region = "Bắc Ninh",
            },
            new Customer
            {
                Id = Guid.NewGuid(),
                Firstname = "Dawn",
                Lastname = "Christensen",
                Address = "P.O. Box 434, 2349 Justo St.",
                City = "Sandviken",
                Country = "Turkey",
                Email = "feugiat.lorem@protonmail.net",
                Phone = "1-324-207-7727",
                PostalCode = "58-59",
                Region = "Puebla",
            }
        };
    }

}
