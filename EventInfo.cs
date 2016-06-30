using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicationSubscription
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
