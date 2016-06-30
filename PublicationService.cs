using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace PublicationService
{
    public class PublicationService
    {
        private Dictionary<int, EventInfo> _publicationDictionary;

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

            foreach (ISubscriptionCallback subscriptionCallback in currentEvent.CallbackList)
            {
                subscriptionCallback.Raise(argSender, argEventArgs);
            }
        }

        //Subscribe to an event
        public void Subscribe(int argEventId, ISubscriptionCallback argCallback)
        {
            EventInfo currentEvent = _publicationDictionary[argEventId];

            currentEvent.CallbackList.Add(argCallback);
        }

        //Release an events subscriptions
        public void ReleaseSubscription()
        {

        }
    }
}
