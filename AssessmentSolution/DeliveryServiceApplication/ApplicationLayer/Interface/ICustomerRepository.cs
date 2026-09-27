using DeliveryServiceApplication.Domain.Model;
using System.Collections.Generic;

namespace DeliveryServiceApplication.ApplicationLayer.Interface
{
    public interface ICustomerRepository
    {
        void AddCustomer(Customer customer);

        IEnumerable<Customer> FetchAllCustomers();
    }
}
