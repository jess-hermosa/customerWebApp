using CustomerWebapp.Models;
using CustomerWebapp.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerWebapp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private const int PageSize = 5;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: CustomerController
        public async Task<ActionResult> Index(int page = 1)
        {
            var customers = await _customerService.GetPaginated(page, PageSize);
            var totalCustomers = await _customerService.TotalCustomers();

            ViewBag.TotalPages = (int)Math.Ceiling((double)totalCustomers / PageSize);
            return View(customers);
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerViewModel customer)
        {
            try
            {
                _customerService.AddCustomer(customer);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var customer = await _customerService.Get(id);
            return View(customer);
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerViewModel customer)
        {
            try
            {
                _customerService.UpdateCustomer(customer);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            var customer = await _customerService.Get(id);
            return View(customer);
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, CustomerViewModel customer)
        {
            try
            {
                _customerService.DeleteCustomer(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
