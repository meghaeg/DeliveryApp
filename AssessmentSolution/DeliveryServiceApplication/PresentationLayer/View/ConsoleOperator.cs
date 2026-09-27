using DeliveryServiceApplication.ApplicationLayer.Service;
using DeliveryServiceApplication.PresentationLayer.Helper;
using System;

namespace DeliveryServiceApplication.PresentationLayer.View
{
    public class ConsoleOperator
    {
        private readonly ParcelService _parcelService;

        private readonly DeliveryService _deliveryService;

        public ConsoleOperator(ParcelService parcelService, DeliveryService deliveryService)
        {
            this._parcelService = parcelService;
            this._deliveryService = deliveryService;
        }

        public void Run()
        {
            int choice;
            do
            {
                Console.WriteLine("1. Admin Dashboard");
                Console.WriteLine("2. Customer Dashboard");
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
                        var adminView = new AdminView(_deliveryService);
                        adminView.RunAdminDashBoard();
                        break;
                    case 2:
                        var parcelView = new ParcelView(_parcelService);
                        parcelView.RunParcelServiceDahsboard();
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
    }
}
