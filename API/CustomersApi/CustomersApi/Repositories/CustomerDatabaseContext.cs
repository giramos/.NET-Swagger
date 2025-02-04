﻿using CustomersApi.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CustomersApi.Repositories
{
    public class CustomerDatabaseContext : DbContext
    {
        public CustomerDatabaseContext(DbContextOptions<CustomerDatabaseContext> options) : base(options)
        {
        }
        public DbSet<CustomerEntity> Customer { get; set; } // acceso a la BBDD

        public async Task<CustomerEntity?> Get(long id)
        {
            return await Customer.FirstOrDefaultAsync(x => x.Id == id); // comunica asincrona
        }

        /// <summary>
        /// Añade un customer a la BBDD
        /// </summary>
        /// <param name="customerDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<CustomerEntity> Add(CreateCustomerDto customerDto)
        {
            CustomerEntity entity = new CustomerEntity()
            {
                Id = null,
                Address = customerDto.Address,
                Email = customerDto.Email,
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName,
                Phone = customerDto.Phone

            };
            EntityEntry<CustomerEntity> response = await Customer.AddAsync(entity);
            await SaveChangesAsync();
            return await Get(response.Entity.Id ?? throw new Exception("no se ha podido añadir"));
        }
        /// <summary>
        /// Borra un customer 
        /// </summary>
        /// <param name="id">Ident del customer</param>
        /// <returns></returns>
        public async Task<bool> Delete(long id)
        {
            var entity = await Get(id);
            Customer.Remove(entity);
            SaveChanges();
            return true;

        }
        public async Task<bool> Actualizar(CustomerEntity customerEntity)
        {
            Customer.Update(customerEntity);
            await SaveChangesAsync();
            return true;
        }
    }

    public class CustomerEntity
    {
        public long? Id { get; set; } // puede ser null
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public CustomerDto ToDto()
        {
            return new CustomerDto
            {
                Address = Address,
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                Phone = Phone,
                Id = Id ?? throw new Exception("El id no puede ser null")
            };
        }
    }
}
