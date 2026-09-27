using DeliveryServiceApplication.ApplicationLayer.Interface;
using DeliveryServiceApplication.Domain.Enums;
using DeliveryServiceApplication.Domain.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DeliveryServiceApplication.ApplicationLayer.Service
{
    public class ParcelService
    {
        private readonly IParcelRepository _parcelRepository;

        public ParcelService(IParcelRepository parcelRepository)
        {
            this._parcelRepository = parcelRepository;
        }

        public void BookParcel(Parcel parcel)
        {
            this._parcelRepository.AddParcel(parcel);
        }

        public IEnumerable<Parcel> GetAllParcels()
        {
            return this._parcelRepository.FetchAllParcels();
        }

        public void UpdateParcel(Parcel parcel)
        {
            var parcels = this.GetAllParcels();
            var parcelToUpdate = parcels.FirstOrDefault(x => x.ParcelId == parcel.ParcelId);
            parcelToUpdate.ParcelStatus = parcel.ParcelStatus;
            this._parcelRepository.UpdateParcelStatus(parcel);
        }

        public Result CancelParcel(int index)
        {
            var parcels = this.GetAllParcels().ToList();
            if (index >= parcels.Count)
            {
                return new Result(false, $"Index Out of range");
            }

            var parcelToUpdate = parcels[index];
            if (parcelToUpdate.ParcelStatus == DeliveryStatus.PickedUp || parcelToUpdate.ParcelStatus == DeliveryStatus.InTransit || parcelToUpdate.ParcelStatus == DeliveryStatus.Delivered)
            {
                return new Result(false, $"Deliver can't be cancelled right now, Parcel is already {parcelToUpdate.ParcelStatus}");
            }

            parcelToUpdate.ParcelStatus = DeliveryStatus.Cancelled;
            this.UpdateParcel(parcelToUpdate);
            return new Result(true, "Parcel Cancelled Successfully");
        }
    }
}
