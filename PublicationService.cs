using System;
using System.Collections.Generic;
using System.Linq;

namespace PublicationSubscriptionService
{
    public class PublicationService : IPublicationService
    {
        private Dictionary<string, Dictionary<Guid, SubscriptionCallback>> _publicationDictionary;

        public delegate void SubscriptionCallback(object argSender, object argEventArgs);

        public PublicationService()
        {
            _publicationDictionary = new Dictionary<string, Dictionary<Guid, SubscriptionCallback>>();
        }

        //Add an event to the published list
        public void Register(string argEventName)
        {
            //Check for events with this name.
            Dictionary<Guid, SubscriptionCallback> currentEvent = _publicationDictionary[argEventName];

            //If there are no events with this name, add one.
            if (currentEvent.Equals(default(Dictionary<Guid, SubscriptionCallback>)))
            {
                _publicationDictionary.Add(argEventName, new Dictionary<Guid, SubscriptionCallback>());
            }
        }

        //Raise an event publication
        public void RaiseEvent(string argEventName, object argSender = null, object argEventArgs = null)
        {
            Dictionary<Guid, SubscriptionCallback> currentEvent = _publicationDictionary[argEventName];

            foreach (KeyValuePair<Guid, SubscriptionCallback> eventSubscription in currentEvent)
            {
                eventSubscription.Value(argSender, argEventArgs);
            }
        }

        //Subscribe to an event
        public Guid Subscribe(string argEventName, SubscriptionCallback argCallback)
        {
            try
            {
                Dictionary<Guid, SubscriptionCallback> currentEvent = _publicationDictionary[argEventName];
                Guid subscriptionGuid = Guid.NewGuid();

                currentEvent.Add(subscriptionGuid, argCallback);
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

            foreach (KeyValuePair<string, Dictionary<Guid, SubscriptionCallback>> currentEvent in _publicationDictionary)
            {
                currentEvent.Value.Remove(argSubscriptionGuid);
            }

        }
    }
}
