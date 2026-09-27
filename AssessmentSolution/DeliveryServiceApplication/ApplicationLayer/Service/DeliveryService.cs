using DeliveryServiceApplication.ApplicationLayer.Interface;
using DeliveryServiceApplication.Domain.Enums;
using DeliveryServiceApplication.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryServiceApplication.ApplicationLayer.Service
{
    public class DeliveryService
    {
        private readonly object _parcelLock = new object();

        private List<Parcel> _pendingDeliveries = new List<Parcel>();

        private readonly IDeliveryAgentRepo _deliveryAgentRepository;

        private readonly IParcelRepository _parcelRepository;

        public DeliveryService(IDeliveryAgentRepo deliveryAgentRepo, IParcelRepository parcelRepository)
        {
            this._deliveryAgentRepository = deliveryAgentRepo;
            this._parcelRepository = parcelRepository;
        }

        public async Task InitialAgentService()
        {
            var agents = this._deliveryAgentRepository.FetchAllDeliveryAgent();
            foreach (var agent in agents)
            {
                if (agent.AgentStatus == DeliveryAgentStatus.Available)
                {
                    _ = DoDeliveryService(agent);
                }
                else if (agent.AgentStatus == DeliveryAgentStatus.Processing)
                {
                    _ = CompleteDeliveryService(agent);
                }
            }
        }

        public void CheckforPendingDeliveries()
        {
            lock (_parcelLock)
            {
                this._pendingDeliveries = this._parcelRepository.FetchAllParcels().Where(x => x.ParcelStatus == DeliveryStatus.WaitingForPickUp).ToList();
            }
        }

        private async Task CompleteDeliveryService(DeliveryAgent agent)
        {
            var parcel = this._parcelRepository.FetchAllParcels().FirstOrDefault(x => x.ParcelId == agent.ParcelId);
            if (parcel.ParcelStatus == DeliveryStatus.PickedUp)
            {
                await Task.Delay(2000);
                parcel.ParcelStatus = DeliveryStatus.InTransit;
                this._parcelRepository.UpdateParcel(parcel);
                await Task.Delay(3000);
                parcel.ParcelStatus = DeliveryStatus.Delivered;
                parcel.DeliveredTime = DateTime.Now;
                this._parcelRepository.UpdateParcel(parcel);
            }
            else if (parcel.ParcelStatus == DeliveryStatus.InTransit)
            {
                await Task.Delay(3000);
                parcel.ParcelStatus = DeliveryStatus.Delivered;
                parcel.DeliveredTime = DateTime.Now;
                this._parcelRepository.UpdateParcel(parcel);
            }

            agent.AgentStatus = DeliveryAgentStatus.Available;
            agent.ParcelId = Guid.Empty;
            this._deliveryAgentRepository.UpdateDeliveryAgentStatus(agent);
        }

        public async Task DoDeliveryService(DeliveryAgent deliveryAgent)
        {
            List<Parcel> parcelToDeliver;
            while (deliveryAgent.AgentStatus == DeliveryAgentStatus.Available)
            {
                lock (_parcelLock)
                {
                    parcelToDeliver = this._pendingDeliveries;
                }

                foreach (var parcel in parcelToDeliver)
                {
                    if (parcel.ParcelStatus == DeliveryStatus.WaitingForPickUp)
                    {
                        deliveryAgent.AgentStatus = DeliveryAgentStatus.Processing;
                        deliveryAgent.ParcelId = parcel.ParcelId;
                        this._deliveryAgentRepository.UpdateDeliveryAgentStatus(deliveryAgent);
                        parcel.PickUpTime = DateTime.Now;
                        parcel.ParcelStatus = DeliveryStatus.PickedUp;
                        parcel.DeliveryAgentId = deliveryAgent.AgentId;
                        this._parcelRepository.UpdateParcel(parcel);
                        await Task.Delay(2000);
                        parcel.ParcelStatus = DeliveryStatus.InTransit;
                        this._parcelRepository.UpdateParcel(parcel);
                        await Task.Delay(3000);
                        parcel.ParcelStatus = DeliveryStatus.Delivered;
                        parcel.DeliveredTime = DateTime.Now;
                        this._parcelRepository.UpdateParcel(parcel);
                        deliveryAgent.AgentStatus = DeliveryAgentStatus.Available;
                        deliveryAgent.ParcelId = Guid.Empty;
                        this._deliveryAgentRepository.UpdateDeliveryAgentStatus(deliveryAgent);
                    }
                }
            }
        }

        public IEnumerable<DeliveryAgent> FetchAllAgents()
        {
            return this._deliveryAgentRepository.FetchAllDeliveryAgent();
        }

        public bool AddAgent(DeliveryAgent deliveryAgent)
        {
            if (this.FetchAllAgents().FirstOrDefault(x => x.AgentId == deliveryAgent.AgentId) != null)
            {
                return false;
            }

            this._deliveryAgentRepository.AddDeliverAgent(deliveryAgent);
            return true;
        }
    }
}
