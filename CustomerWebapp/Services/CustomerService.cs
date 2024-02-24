using CustomerWebapp.Models;
using Microsoft.Extensions.Options;

namespace CustomerWebapp.Services
{
    public interface ICustomerService
    {
        public Task<List<CustomerViewModel>> GetPaginated(int page, int pageSize);
        public Task<CustomerViewModel> Get(Guid id);
        public void AddCustomer(CustomerViewModel customer);
        public void UpdateCustomer(CustomerViewModel customer);
        public void DeleteCustomer(Guid id);
        public Task<int> TotalCustomers();
    }

    internal class CustomerService : ICustomerService
    {
        private readonly IHttpService _httpService;
        private string Url;
        private string CacheKey = "customersKey";

        public CustomerService(IHttpService httpService,
            IOptions<ApiSettings> apiSettings)
        {
            _httpService = httpService;
            Url = $"{apiSettings.Value.BaseUrl}/{apiSettings.Value.Endpoint}";
        }


        public async Task<List<CustomerViewModel>> GetPaginated(int page, int pageSize)
        {
            try
            {
                var customers = await _httpService.GetAsync<IEnumerable<CustomerViewModel>>($"{Url}?page={page}&pageSize={pageSize}");

                return customers.ToList();
            }
            catch (Exception)
            {
                throw new HttpRequestException($"Error occured when trying to customers");
            }
        }

        public async Task<CustomerViewModel> Get(Guid id)
        {
            var existingCustomer = await _httpService.GetAsync<CustomerViewModel>($"{Url}/{id}");
            if (existingCustomer != null)
            {
                return existingCustomer;
            }
            else
            {
                throw new KeyNotFoundException("Customer not found");
            }
        }

        public async void AddCustomer(CustomerViewModel customer)
        {
            var result = await _httpService.PostAsync(Url, customer);
            if (!result) 
            {
                throw new HttpRequestException($"Error occured when trying to {nameof(AddCustomer)}");
            }
        }

        public async void UpdateCustomer(CustomerViewModel customer)
        {
            var result = await _httpService.PostAsync($"{Url}/{customer.Id}", customer);
            if (!result)
            {
                throw new HttpRequestException($"Error occured when trying to {nameof(UpdateCustomer)}");
            }
        }

        public async void DeleteCustomer(Guid id)
        {
            var result = await _httpService.DeleteAsync($"{Url}/{id}");
            if (!result)
            {
                throw new HttpRequestException($"Error occured when trying to {nameof(DeleteCustomer)}");
            }
        }

        public async Task<int> TotalCustomers()
        {
           return await _httpService.GetAsync<int>($"{Url}/gettotalcustomers");
        }
    }

}
