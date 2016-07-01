using System;

namespace PublicationSubscriptionService
{
    public interface IPublicationService
    {
        void RaiseEvent(string argEventName, object argSender = null, object argEventArgs = null);
        void ReleaseSubscription(Guid argSubscriptionGuid);
        Guid Subscribe(string argEventName, PublicationService.SubscriptionCallback argCallback);
    }
}
