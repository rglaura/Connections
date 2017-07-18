using Entities.Entities;
using System.Collections.Generic;

namespace Entities.Contracts
{
    public interface IReceiver : INode
    {
        void Receive(List<Information> information);
        void SendTarget(List<Information> information);
        void UpdateState(List<Information> information);
        bool CanReceive();
        int GetNumberOfHoles();
        void ReturnInformation(Information information);
        List<Information> GetVisitors();
    }
}
