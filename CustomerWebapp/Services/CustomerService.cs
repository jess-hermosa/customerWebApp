using CustomerWebapp.Models;

namespace CustomerWebapp.Services
{
    public interface ICustomerService
    {
        public Task<List<CustomerViewModel>> GetPaginated(int page, int pageSize);
        public Task<CustomerViewModel> Get(int id);
        public void AddCustomer(CustomerViewModel customer);
        public void UpdateCustomer(CustomerViewModel customer);
        public void DeleteCustomer(int id);
        public Task<int> TotalCustomers();
    }

    public class CustomerService : ICustomerService
    {
        private readonly IHttpService _httpService;
        private readonly ICacheService _cacheService;
        private const string BaseUrl = "https://localhost:7126";
        private const string Endpoint = "/api/customer";
        private string Url;
        private string CacheKey = "customersKey";

        public CustomerService(IHttpService httpService, ICacheService cacheService)
        {
            _httpService = httpService;
            _cacheService = cacheService;
            Url = $"{BaseUrl}/{Endpoint}";
        }


        public async Task<List<CustomerViewModel>> GetPaginated(int page, int pageSize)
        {
            var customers = await _cacheService.GetOrSetAsync(CacheKey, 
                async () => await _httpService.GetAsync<IEnumerable<CustomerViewModel>>(Url));

            var paginatedCustomer = customers
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return paginatedCustomer;
        }

        public async Task<CustomerViewModel> Get(int id)
        {
            var existingCustomer = await _httpService.GetAsync<CustomerViewModel>($"{Url}/{id}");
            if (existingCustomer != null)
            {
                return existingCustomer;
            }
            else
            {
                throw new ArgumentException("Customer not found");
            }
        }

        public async void AddCustomer(CustomerViewModel customer)
        {
            await _httpService.PostAsync(Url, customer);
        }

        public async void UpdateCustomer(CustomerViewModel customer)
        {
            await _httpService.PostAsync($"{Url}/{customer.Id}", customer);
        }

        public async void DeleteCustomer(int id)
        {
            await _httpService.DeleteAsync($"{Url}/{id}");
        }

        public async Task<int> TotalCustomers()
        {
            var customers = await _cacheService.GetOrSetAsync(CacheKey,
                async () => await _httpService.GetAsync<IEnumerable<CustomerViewModel>>(Url));

            return customers.Count();
        }
    }

}
