using DeliveryServiceApplication.Domain.Enums;
using System;

namespace DeliveryServiceApplication.Domain.Model
{
    public class Parcel
    {
        public Parcel()
        { 
        }

        public Parcel(Guid parcelId, Guid customerId, string source, string destination, string parcelType, DateTime bookedTime, DeliveryStatus parcelStatus)
        {
            this.ParcelId = parcelId;
            this.CustomerId = customerId;
            this.Source = source;
            this.Destination = destination;
            this.ParcelType = parcelType;
            this.BookedTime = bookedTime;
            this.ParcelStatus = parcelStatus;
        }

        public int DeliveryAgentId { get; set; }

        public Guid ParcelId { get; set; }

        public Guid CustomerId { get; set; }

        public string Source { get; set; }

        public string Destination { get; set; }

        public string ParcelType { get; set; }

        public DateTime BookedTime { get; set; }

        public DateTime PickUpTime { get; set; }

        public DateTime DeliveredTime { get; set; }

        public DeliveryStatus ParcelStatus { get; set; }
    }
}
