using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicationService
{
    public interface ISubscriptionCallback
    {
        void Raise(object argSender, object argEventArgs);
    }
}
