using DeliveryServiceApplication.ApplicationLayer.Interface;
using DeliveryServiceApplication.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace DeliveryServiceApplication.InfrastructureLayer
{
    public class DeliveryAgentRepository : IDeliveryAgentRepo
    {
        private readonly object _fileLock = new object();

        public void AddDeliverAgent(DeliveryAgent deliveryAgent)
        {
            lock (_fileLock)
            {
                var listOfAgents = FileHandlingService.ReadFile<DeliveryAgent>(FilePath.DeliveryAgentFile);
                listOfAgents.Add(deliveryAgent);
                FileHandlingService.WriteFile(FilePath.DeliveryAgentFile, listOfAgents);
            }
        }

        public IEnumerable<DeliveryAgent> FetchAllDeliveryAgent()
        {
            lock (_fileLock)
            {
                var listOfAgents = FileHandlingService.ReadFile<DeliveryAgent>(FilePath.DeliveryAgentFile);
                return listOfAgents;
            }
        }

        public void UpdateDeliveryAgentStatus(DeliveryAgent deliveryAgent)
        {
            lock (_fileLock)
            {
                var listOfAgents = FileHandlingService.ReadFile<DeliveryAgent>(FilePath.DeliveryAgentFile);
                var agent = listOfAgents.FirstOrDefault(x => x.AgentId == deliveryAgent.AgentId);
                agent.AgentStatus = deliveryAgent.AgentStatus;
                agent.ParcelId = deliveryAgent.ParcelId;
                FileHandlingService.WriteFile(FilePath.DeliveryAgentFile, listOfAgents);
            }
        }
    }
}
