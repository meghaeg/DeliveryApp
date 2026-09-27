using DeliveryServiceApplication.ApplicationLayer.Interface;
using DeliveryServiceApplication.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace DeliveryServiceApplication.InfrastructureLayer
{
    public class ParcelRepository : IParcelRepository
    {
        private readonly object _fileLock = new object();
        public void AddParcel(Parcel parcel)
        {
            lock (_fileLock)
            {
                var listOfParcels = FileHandlingService.ReadFile<Parcel>(FilePath.ParcelFile);
                listOfParcels.Add(parcel);
                FileHandlingService.WriteFile(FilePath.ParcelFile, listOfParcels);
            }
        }

        public IEnumerable<Parcel> FetchAllParcels()
        {
            lock (_fileLock)
            {
                var listOfParcels = FileHandlingService.ReadFile<Parcel>(FilePath.ParcelFile);
                return listOfParcels;
            }
        }

        public void UpdateParcel(Parcel parcel)
        {
            lock (_fileLock)
            {
                var listOfParcels = FileHandlingService.ReadFile<Parcel>(FilePath.ParcelFile);
                var parceltoUpdate = listOfParcels.FirstOrDefault(x => x.ParcelId == parcel.ParcelId);
                parceltoUpdate.ParcelStatus = parcel.ParcelStatus;
                parceltoUpdate.PickUpTime = parcel.PickUpTime;
                parceltoUpdate.DeliveredTime = parcel.DeliveredTime;
                parceltoUpdate.DeliveryAgentId = parcel.DeliveryAgentId;
                FileHandlingService.WriteFile(FilePath.ParcelFile, listOfParcels);
            }
        }
    }
}
