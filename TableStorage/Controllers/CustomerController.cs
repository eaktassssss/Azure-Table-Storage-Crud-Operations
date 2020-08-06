using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TableStorage.Entities;
using TableStorage.Repository.Abstract;

namespace TableStorage.Controllers
{
    public class CustomerController :Controller
    {

        ITableStorageRepository<Customers> _tableStorageRepository;
        public CustomerController(ITableStorageRepository<Customers> tableStorageRepository)
        {
            _tableStorageRepository = tableStorageRepository;
        }
        [Route("customer-list")]
        [HttpGet]
        public ActionResult Index()
        {
            var response = _tableStorageRepository.QueryAsync().ToList();
            return View(response);
        }
        [Route("customer-add")]
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [Route("customer-add")]
        [HttpPost]
        public async Task<ActionResult> Add(Customers customers)
        {
            //customers.Timestamp = DateTime.Now;
            customers.CustomerCode = Guid.NewGuid().ToString();
            customers.RowKey = Guid.NewGuid().ToString();
            customers.PartitionKey = "CustomerType";
            var response = await _tableStorageRepository.AddAsync(customers);
            return RedirectToAction("Index");
        }
        [Route("customer-update")]
        [HttpGet]
        public async Task<ActionResult> Update(string rowKey, string partitionKey)
        {
            var response = await _tableStorageRepository.GetAsync(partitionKey, rowKey);
            return View(response);
        }
        [Route("customer-update")]
        [HttpPost]
        public async Task<ActionResult> Update(Customers customers)
        {
            var response = await _tableStorageRepository.UpdateAsync(customers);
            return RedirectToAction("Index");
        }
        [Route("customer-delete")]
        public async Task<ActionResult> Delete(string partitionKey, string rowKey)
        {
            var response = await _tableStorageRepository.DeleteAsync(partitionKey, rowKey);
            return RedirectToAction("Index");
        }
    }
}