using DeliveryServiceApplication.ApplicationLayer.Service;
using DeliveryServiceApplication.Domain.Enums;
using DeliveryServiceApplication.Domain.Model;
using DeliveryServiceApplication.PresentationLayer.Helper;
using System;

namespace DeliveryServiceApplication.PresentationLayer.View
{
    public class AdminView
    {
        private readonly DeliveryService _deliveryService;

        public AdminView(DeliveryService deliveryService)
        {
            this._deliveryService = deliveryService;
        }

        public void RunAdminDashBoard()
        {
            int choice;
            do
            {
                Console.WriteLine("1. Add Delivery Agent");
                Console.WriteLine("2. View delivery Agent");
                Console.WriteLine("3. Exit");
                Console.WriteLine("Enter your choice (1/2/3)");
                bool isValidChoice = InputValidator.ValidateInteger(Console.ReadLine(), out choice);
                if (!isValidChoice)
                {
                    choice = 0;
                }

                switch (choice)
                {
                    case 1:
                        this.AddAgent();
                        break;
                    case 2:
                        this.ViewAgents();
                        break;
                    case 3:
                        Console.WriteLine("Exiting the App, Bye!");
                        break;
                    default:
                        Console.WriteLine("Invalid Choice, Enter a valid choice between 1 to 4");
                        break;
                }

            }
            while (choice != 3);
        }

        private void AddAgent()
        {
            Console.WriteLine("Enter agent ID(Integer): ");
            var isValidId = InputValidator.ValidateInteger(Console.ReadLine(), out int agentId);
            if (!isValidId)
            {
                Console.WriteLine("Invalid Agent Id format enter a integer number!");
                return;
            }

            bool isAdded = this._deliveryService.AddAgent(new DeliveryAgent(agentId, DeliveryAgentStatus.Available));
            if (!isAdded)
            {
                Console.WriteLine("Delivery Agent has been already added, can't add!");
                return;
            }

            Console.WriteLine("Delivery agent added Successfully!");
        }

        private void ViewAgents()
        {
            var agents = this._deliveryService.FetchAllAgents();
            foreach (var agent in agents)
            {
                Console.WriteLine($"Agent Id: {agent.AgentId}, Agent status: {agent.AgentStatus}");
                if (agent.AgentStatus == DeliveryAgentStatus.Processing)
                {
                    Console.WriteLine($"Parcel Id: {agent.ParcelId}");
                }
            }
        }
    }
}
