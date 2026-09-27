using DeliveryServiceApplication.ApplicationLayer.Service;

namespace DeliveryServiceApplication.PresentationLayer.View
{
    public class ConsoleOperator
    {
        private readonly ParcelService _parcelService;

        public ConsoleOperator(ParcelService parcelService)
        {
            this._parcelService = parcelService;
        }

        public void Run()
        {
            var parcelView = new ParcelView(_parcelService);
            parcelView.RunParcelServiceDahsboard();
        }
    }
}
