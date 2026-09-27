using DeliveryServiceApplication.ApplicationLayer.Service;
using DeliveryServiceApplication.Domain.Enums;
using DeliveryServiceApplication.Domain.Model;
using DeliveryServiceApplication.PresentationLayer.Helper;
using System;
using System.Linq;

namespace DeliveryServiceApplication.PresentationLayer.View
{
    public class ParcelView
    {
        private readonly ParcelService _parcelService;

        public ParcelView(ParcelService parcelService)
        {
            this._parcelService = parcelService;
        }

        public void RunParcelServiceDahsboard()
        {
            int choice;
            do
            {
                Console.WriteLine("1. Book Parcel");
                Console.WriteLine("2. Cancel Parcel");
                Console.WriteLine("3. View Parcel Status");
                Console.WriteLine("4. Exit");
                Console.WriteLine("Enter your choice (1/2/3/4)");
                bool isValidChoice = InputValidator.ValidateInteger(Console.ReadLine(), out choice);
                if (!isValidChoice)
                {
                    choice = 0;
                }

                switch (choice)
                {
                    case 1:
                        this.BookParcel();
                        break;
                    case 2:
                        this.CancelParcel();
                        break;
                    case 3:
                        this.ViewAllParcels();
                        break;
                    case 4:
                        Console.WriteLine("Exiting the App, Bye!");
                        break;
                    default:
                        Console.WriteLine("Invalid Choice, Enter a valid choice between 1 to 4");
                        break;
                }

            }
            while (choice != 4) ;
        }

        private void BookParcel()
        {
            var parcel = this.GetParcelDetails();
            if (parcel == null)
            {
                return;
            }

            this._parcelService.BookParcel(parcel);
            Console.WriteLine("Parcel Booked Successfully!");
        }

        private Parcel GetParcelDetails()
        {
            var parcelType = this.GetValidString("Parcel type");
            if (parcelType == null)
            {
                return null;
            }

            var source = this.GetValidString("Source Location");
            if (source == null)
            {
                return null;
            }

            var destination = this.GetValidString("Destination Location");
            if (destination == null)
            {
                return null;
            }

            return new Parcel(Guid.NewGuid(), CurrentSessionManager.CurrentSessionUserId, source, destination, parcelType, DateTime.Now, DeliveryStatus.Booked);
        }

        private string GetValidString(string title)
        {
            Console.WriteLine($"Enter {title}: ");
            string input = Console.ReadLine();
            var isValidString = InputValidator.ValidateString(input);
            if (!isValidString)
            {
                Console.WriteLine("Invalid string format!");
                return null;
            }

            return input;
        }

        private void CancelParcel()
        {
            this.ViewAllParcels();
            Console.WriteLine("Enter index of parcel to be cancelled: ");
            var isValidIndex = InputValidator.ValidateInteger(Console.ReadLine(), out int index);
            if (!isValidIndex || index < 0)
            {
                Console.WriteLine("Invalid index");
                return;
            }

            var isCancelled = this._parcelService.CancelParcel(index - 1);
            Console.WriteLine($"{isCancelled.Message}");
        }

        private void ViewAllParcels()
        {
            var listOfParcels = this._parcelService.GetAllParcels();
            if (!listOfParcels.Any())
            {
                Console.WriteLine("No parcels found!");
                return;
            }

            Console.WriteLine("YOUR PARCELS");
            int index = 1;
            foreach (var parcel in listOfParcels)
            {
                Console.WriteLine($" {index++}. Parcel Type: {parcel.ParcelType}, Source : {parcel.Source}, Destination: {parcel.Destination}, Booked Time: {parcel.BookedTime}, Delivery Status: {parcel.ParcelStatus}");
                if (parcel.ParcelStatus == DeliveryStatus.Delivered)
                {
                    Console.WriteLine($"PickUp Time: {parcel.PickUpTime}, Delivery Time: {parcel.DeliveredTime}");
                }
                else if (parcel.ParcelStatus == DeliveryStatus.PickedUp)
                {
                    Console.WriteLine($"PickUp Time: {parcel.PickUpTime}");
                }
            }
        }
    }
}

