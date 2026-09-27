using DeliveryServiceApplication.ApplicationLayer.Service;
using DeliveryServiceApplication.InfrastructureLayer;
using DeliveryServiceApplication.PresentationLayer.View;
using System;

namespace DeliveryServiceApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;
            var parcelRepository = new ParcelRepository();
            var parcelService = new ParcelService(parcelRepository);
            var consoleOperator = new ConsoleOperator(parcelService);
            consoleOperator.Run();
        }

        private static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex.Message}");
            }
        }
    }
}
