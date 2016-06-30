using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using PublicationService;

namespace PublicationSubscription
{
    public class PublicationService:IPublicationService
    {
        private Dictionary<int, EventInfo> _publicationDictionary;

        public delegate void SubscriptionCallback(object argSender, object argEventArgs);

        public PublicationService()
        {
            _publicationDictionary = new Dictionary<int, EventInfo>();
        }

        //Add an event to the published list
        public int Register(string argEventName)
        {
            //Check for events with this name.
            KeyValuePair<int, EventInfo> currentEvent = _publicationDictionary.SingleOrDefault(x => x.Value.Name == argEventName);

            //If there are no events with this name, add one.
            if (currentEvent.Equals(default(KeyValuePair<int, EventInfo>)))
            {
                _publicationDictionary.Add(_publicationDictionary.Count, new EventInfo(argEventName));
                return _publicationDictionary.Count;
            }

            return currentEvent.Key;
        }

        //Raise an event publication
        public void RaiseEvent(int argEventId, object argSender = null, object argEventArgs = null)
        {
            EventInfo currentEvent = _publicationDictionary[argEventId];

            foreach (KeyValuePair<Guid, SubscriptionCallback> eventSubscription in currentEvent.SubscriptionList)
            {
                eventSubscription.Value(argSender, argEventArgs);
            }
        }

        //Subscribe to an event
        public Guid Subscribe(int argEventId, SubscriptionCallback argCallback)
        {
            try
            {
                EventInfo currentEvent = _publicationDictionary[argEventId];
                Guid subscriptionGuid = Guid.NewGuid();

                currentEvent.SubscriptionList.Add(subscriptionGuid, argCallback);
                return subscriptionGuid;
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
        }

        //Release an events subscriptions
        public void ReleaseSubscription(Guid argSubscriptionGuid)
        {

            foreach (KeyValuePair<int, EventInfo> currentEvent in _publicationDictionary)
            {
                currentEvent.Value.SubscriptionList.Remove(argSubscriptionGuid);
            }

        }
    }
}
