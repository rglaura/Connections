using Entities.Contracts;
using Entities.Entities;
using System.Collections.Generic;

namespace Entities.Contracts
{
    public interface IEmitter : INode
    {
        void SetReachables(List<IReceiver> reachables);
        void TrySend(IReceiver target);
        void AddInformationToStock(List<Information> information);
        void DecreaseStock();
        bool DoIHaveConnectionWith(IReceiver receiver);
        bool DoIHaveConnectionWith(IReceiver receiver, List<Information> information);
        void ReclaimInformation(Information information);
    }
}
