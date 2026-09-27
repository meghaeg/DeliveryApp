using DeliveryServiceApplication.Domain.Enums;
using System;

namespace DeliveryServiceApplication.Domain.Model
{
    public class DeliveryAgent
    {
        public DeliveryAgent()
        {
        }

        public DeliveryAgent(int agentId, DeliveryAgentStatus deliveryAgentStatus)
        {
            this.AgentId = agentId;
            this.AgentStatus = deliveryAgentStatus;
        }

        public int AgentId { get; set; }

        public DeliveryAgentStatus AgentStatus { get; set; }

        public Guid ParcelId { get; set; }
    }
}
