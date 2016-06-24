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
        private Dictionary<Guid, EventInfo> _publicationDictionary;

        public PublicationService()
        {
            _publicationDictionary = new Dictionary<Guid, EventInfo>();
        }

        //Add an event to the published list
        public void Add(Guid argEventGuid, string argEventName)
        {
            _publicationDictionary.Add(argEventGuid, new EventInfo(argEventName));
        }

        public void Add(string argEventName)
        {
            Guid tempGuid = new Guid();
            Add(tempGuid, argEventName);
        }

        //Get list of publishable events
        public Dictionary<Guid, string> EventList()
        {
            return _publicationDictionary.ToDictionary(x => x.Key, y => y.Value.Name);
        }

        public Dictionary<Guid, string> EventList(string argNameFilter)
        {
            return _publicationDictionary
                .Where(argX => argX.Value.Name.Contains(argNameFilter)) //Where the Value
                .ToDictionary(argY => argY.Key, argZ => argZ.Value.Name);
        }

        //Raise an event publication
        public void RaiseEvent(Guid argEventGuid, object argSender = null, object argEventArgs = null)
        {
            EventInfo currentEvent = _publicationDictionary[argEventGuid];

            foreach (ISubscriptionCallback subscriptionCallback in currentEvent.CallbackList)
            {
                subscriptionCallback.Raise(argSender, argEventArgs);
            }
        }

        //Subscribe to an event
        public void Subscribe(Guid argEventGuid, ISubscriptionCallback argCallback)
        {
            EventInfo currentEvent = _publicationDictionary[argEventGuid];

            currentEvent.CallbackList.Add(argCallback);
        }

        //Release an events subscriptions
        public void ReleaseSubscription()
        {
            
        }
    }
}
