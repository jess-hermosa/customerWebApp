using CustomerWebapp.Models;
using Microsoft.Extensions.Options;

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

    internal class CustomerService : ICustomerService
    {
        private readonly IHttpService _httpService;
        private readonly ICacheService _cacheService;
        private string Url;
        private string CacheKey = "customersKey";

        public CustomerService(IHttpService httpService,
            ICacheService cacheService,
            IOptions<ApiSettings> apiSettings)
        {
            _httpService = httpService;
            _cacheService = cacheService;
            Url = $"{apiSettings.Value.BaseUrl}/{apiSettings.Value.Endpoint}";
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
            var result = await _httpService.PostAsync<CustomerViewModel, IEnumerable<CustomerViewModel>>(Url, customer);
            if (result != null) 
            {
                _cacheService.Update(CacheKey, result);
            }
            else
            {
                throw new ArgumentException($"Error occured when trying to {nameof(AddCustomer)}");
            }
        }

        public async void UpdateCustomer(CustomerViewModel customer)
        {
            var result = await _httpService.PostAsync<CustomerViewModel, IEnumerable<CustomerViewModel>>($"{Url}/{customer.Id}", customer);
            if (result != null)
            {
                _cacheService.Update(CacheKey, result);
            }
            else
            {
                throw new ArgumentException($"Error occured when trying to {nameof(UpdateCustomer)}");
            }
        }

        public async void DeleteCustomer(int id)
        {
            var result = await _httpService.DeleteAsync<int, IEnumerable<CustomerViewModel>>($"{Url}/{id}");
            if (result != null)
            {
                _cacheService.Update(CacheKey, result);
            }
            else
            {
                throw new ArgumentException($"Error occured when trying to {nameof(DeleteCustomer)}");
            }
        }

        public async Task<int> TotalCustomers()
        {
            var customers = await _cacheService.GetOrSetAsync(CacheKey,
                async () => await _httpService.GetAsync<IEnumerable<CustomerViewModel>>(Url));

            return customers.Count();
        }
    }

}
