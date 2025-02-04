using CustomersApi.Dtos;
using CustomersApi.Repositories;

namespace CustomersApi.CasosDeUso
{
    public interface IUpdateCustomerUseCase
    {
        Task<CustomerDto> Execute(CustomerDto customer);
    }
    public class UpdateCustomerUseCase: IUpdateCustomerUseCase
    {
        private readonly CustomerDatabaseContext _customerDatabaseContext;

        public UpdateCustomerUseCase(CustomerDatabaseContext customerDatabaseContext)
        {
            _customerDatabaseContext = customerDatabaseContext;
        }

        public async Task<CustomerDto> Execute(CustomerDto customer)
        {
            //llamaar a la BBDD
            var entity = await _customerDatabaseContext.Get(customer.Id);

            if (entity == null)
            {
                return null;

            }
            entity.FirstName = customer.FirstName;
            entity.LastName = customer.LastName;
            entity.Address = customer.Address;
            entity.Phone = customer.Phone;
            entity.Email = customer.Email;

            await _customerDatabaseContext.Actualizar(entity);
            return entity.ToDto();
        }

    }
}

