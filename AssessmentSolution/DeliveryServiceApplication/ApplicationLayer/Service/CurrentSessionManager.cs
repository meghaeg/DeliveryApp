using System;

namespace DeliveryServiceApplication.ApplicationLayer.Service
{
    public static class CurrentSessionManager
    {
        public static Guid CurrentSessionUserId { get; set; }
    }
}
