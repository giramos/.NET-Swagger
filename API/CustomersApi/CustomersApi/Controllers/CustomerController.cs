﻿using CustomersApi.CasosDeUso;
using CustomersApi.Dtos;
using CustomersApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomersApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly CustomerDatabaseContext _customerDatabaseContext;
        private readonly IUpdateCustomerUseCase _updateCustomerUseCase;

        public CustomerController(CustomerDatabaseContext customerDatabaseContext,
            IUpdateCustomerUseCase updateCustomerUseCase)
        {
            this._customerDatabaseContext = customerDatabaseContext;
            this._updateCustomerUseCase = updateCustomerUseCase;
        }

        //api/customer/
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CustomerDto>))]

        public async Task<IActionResult> GetCustomers()
        {
            var result = _customerDatabaseContext.Customer.Select(c => c.ToDto()).ToList();
            return new OkObjectResult(result);
        }
        //api/customer/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> GetCustomer(long id)
        {
            //var vacio = new CustomerDto();
            CustomerEntity result = await _customerDatabaseContext.Get(id);
            return new OkObjectResult(result.ToDto());
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]

        public async Task<IActionResult> DeleteCustomer(long id)
        {
            var result = await _customerDatabaseContext.Delete(id);
            return new OkObjectResult(result);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CustomerDto))]

        public async Task<IActionResult> CreateCustomer(CreateCustomerDto customer)
        {
            CustomerEntity result = await _customerDatabaseContext.Add(customer);
            //var vacio = new CustomerDto();
            return new CreatedResult($"https://localhost:7163/api/customer/{result.Id}", null);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCustomer(CustomerDto customer)
        {
            CustomerDto? result = await _updateCustomerUseCase.Execute(customer);
            if (result == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(result);
        }

    }
}
