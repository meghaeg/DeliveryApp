using DeliveryServiceApplication.Domain.Model;
using System.Collections.Generic;

namespace DeliveryServiceApplication.ApplicationLayer.Interface
{
    public interface IParcelRepository
    {
        void AddParcel(Parcel parcel);

        IEnumerable<Parcel> FetchAllParcels();

        void UpdateParcel(Parcel parcel);
    }
}
