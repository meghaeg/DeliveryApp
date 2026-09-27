using DeliveryServiceApplication.ApplicationLayer.Service;
using DeliveryServiceApplication.InfrastructureLayer;
using DeliveryServiceApplication.PresentationLayer.View;
using System;
using System.Timers;

namespace DeliveryServiceApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;
            var parcelRepository = new ParcelRepository();
            var deliveryAgentRepo = new DeliveryAgentRepository();
            var deliveryService = new DeliveryService(deliveryAgentRepo, parcelRepository);
            deliveryService.CheckforPendingDeliveries();
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 3000;
            timer.Elapsed += ((object sender, ElapsedEventArgs eventArgs) =>
            {
                deliveryService.CheckforPendingDeliveries();
                _ = deliveryService.InitialAgentService();
            });
            timer.Start();
            var parcelService = new ParcelService(parcelRepository);
            var consoleOperator = new ConsoleOperator(parcelService, deliveryService);
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
