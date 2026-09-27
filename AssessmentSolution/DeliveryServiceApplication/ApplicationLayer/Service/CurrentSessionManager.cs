using System;

namespace DeliveryServiceApplication.ApplicationLayer.Service
{
    public static class CurrentSessionManager
    {
        public static Guid CurrentSessionUserId { get; set; } = Guid.Parse("7f3a9c21-5d84-4e67-b2f1-9a6c3d8e7410");
    }
}
