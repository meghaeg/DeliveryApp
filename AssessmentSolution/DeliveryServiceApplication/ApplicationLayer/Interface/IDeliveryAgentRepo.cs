using DeliveryServiceApplication.Domain.Model;
using System.Collections.Generic;

namespace DeliveryServiceApplication.ApplicationLayer.Interface
{
    public interface IDeliveryAgentRepo
    {
        void AddDeliverAgent(DeliveryAgent deliveryAgent);

        IEnumerable<DeliveryAgent> FetchAllDeliveryAgent();

        void UpdateDeliveryAgentStatus(DeliveryAgent deliveryAgent);
    }
}
