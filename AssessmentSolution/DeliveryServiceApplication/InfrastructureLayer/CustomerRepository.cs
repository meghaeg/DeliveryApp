using DeliveryServiceApplication.ApplicationLayer.Interface;
using DeliveryServiceApplication.Domain.Model;
using System.Collections.Generic;

namespace DeliveryServiceApplication.InfrastructureLayer
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly object _fileLock = new object();

        public void AddCustomer(Customer customer)
        {
            lock (_fileLock)
            {
                var listOfCustomers = FileHandlingService.ReadFile<Customer>(FilePath.CustomerFile);
                listOfCustomers.Add(customer);
                FileHandlingService.WriteFile(FilePath.CustomerFile, listOfCustomers);
            }
        }

        public IEnumerable<Customer> FetchAllCustomers()
        {
            lock (_fileLock)
            {
                var listOfCustomers = FileHandlingService.ReadFile<Customer>(FilePath.CustomerFile);
                return listOfCustomers;
            }
        }
    }
}
