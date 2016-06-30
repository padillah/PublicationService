using System;
using System.Collections.Generic;

namespace PublicationSubscriptionService
{
    public class EventInfo
    {
        public string Name { get; }

        public Dictionary<Guid, PublicationService.SubscriptionCallback> SubscriptionList { get; set; }

        public EventInfo(string argEventName)
        {
            Name = argEventName;
            SubscriptionList = new Dictionary<Guid, PublicationService.SubscriptionCallback>();
        }
    }
}
