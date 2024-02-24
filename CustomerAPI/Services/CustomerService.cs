using CustomerAPI.Models;

namespace CustomerAPI.Services
{
    public interface ICustomerService
    {
        public Task<List<Customer>> GetAll();
        public Task<Customer> Get(int id);
        public void AddCustomer(Customer customer);
        public void UpdateCustomer(Customer customer);
        public void DeleteCustomer(int id);
        public Task<int> TotalCustomers { get; }
    }

    public class CustomerService : ICustomerService
    {

        public Task<List<Customer>> GetAll()
        {
            return Task.FromResult(Customers);
        }

        public Task<Customer> Get(int id)
        {
            var existingCustomer = Customers.FirstOrDefault(i => i.Id == id);
            if (existingCustomer != null)
            {
                return Task.FromResult(existingCustomer);
            }
            else
            {
                throw new ArgumentException("Customer not found");
            }
        }

        public void AddCustomer(Customer customer)
        {
            customer.Id = Customers.Count + 1;
            Customers.Add(customer);
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
                throw new ArgumentException("Customer not found");
            }
        }

        public void DeleteCustomer(int id)
        {
            var customerToRemove = Customers.FirstOrDefault(customer => customer.Id == id);
            if (customerToRemove != null)
            {
                Customers.Remove(customerToRemove);
            }
            else
            {
                throw new ArgumentException("Customer not found");
            }
        }

        public Task<int> TotalCustomers => Task.FromResult(Customers.Count());

        private static List<Customer> Customers = new List<Customer>
        {
            new Customer
            {
                Id = 1,
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
                Id = 2,
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
                Id = 3,
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
                Id = 4,
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
                Id = 5,
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
                Id = 6,
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
                Id = 7,
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
                Id = 8,
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
