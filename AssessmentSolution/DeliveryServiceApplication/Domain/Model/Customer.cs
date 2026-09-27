using System;

namespace DeliveryServiceApplication.Domain.Model
{
    public class Customer
    {
        public Customer()
        {
        }

        public Customer(Guid customerId, string userName, string phnNumber, string password)
        {
            this.CustomerId = customerId;
            this.UserName = userName;
            this.PhnNumber = phnNumber;
            this.Password = password;
        }

        public Guid CustomerId { get; set; }

        public string UserName { get; set; }

        public string PhnNumber { get; set; }

        public string Password { get; set; }
    }
}
