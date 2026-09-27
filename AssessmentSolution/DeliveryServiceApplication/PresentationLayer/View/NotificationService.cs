using DeliveryServiceApplication.Domain.Model;
using DeliveryServiceApplication.PresentationLayer.Helper;
using System;

namespace DeliveryServiceApplication.PresentationLayer.View
{
    public class NotificationService
    {
        private static readonly object _consoleLock = new object();

        public static void ShowNotification(Parcel parcel)
        {
            lock (_consoleLock)
            {
                int currentLeft = Console.CursorLeft;
                int currentTop = Console.CursorTop;
                int left = Console.WindowWidth - 65;
                Console.SetCursorPosition(left, 0);
                TextColor.WriteColorLine($"Your parcel {parcel.ParcelId} has been {parcel.ParcelStatus}".PadRight(60), ConsoleColor.Cyan);
                Console.SetCursorPosition(currentLeft, currentTop);
            }
        }
    }
}
