using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicationService
{
    public class EventInfo
    {
        public string Name { get; }

        public List<ISubscriptionCallback> CallbackList { get; set; }

        public EventInfo(string argEventName)
        {
            Name = argEventName;
        }
    }
}
