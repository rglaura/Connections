using Entities.Contracts;
using Entities.Exceptions;
using Entities.Loggers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System;

namespace Entities.Entities
{
    public partial class Transceiver : Node, ITransceiver
    {
        public bool CanReceive()
        {
            return _receiver.CanReceive();
        }

        public int GetNumberOfHoles()
        {
            return _receiver.GetNumberOfHoles();
        }

        public void Receive(List<Information> information)
        {
            SendTarget(information);
            UpdateState(information);
            AddInformationToStock(information);

            Propagate();
        }

        public void SendTarget(List<Information> information)
        {
            foreach (var info in information)
            {
                info.AddTarget(this);
            }
        }

        public void UpdateState(List<Information> information)
        {
            _receiver.UpdateState(information);
        }

        public void TrySend(IReceiver target)
        {
            _emitter.TrySend(target);
        }

        public void ReturnInformation(Information information)
        {
            _receiver.ReturnInformation(information);
            DecreaseStock();
        }

        public void ReclaimInformation(Information information)
        {
            _emitter.ReclaimInformation(information);
        }

        public void SetReachables(List<IReceiver> reachables)
        {
            _emitter.SetReachables(reachables);
        }

        public void AddInformationToStock(List<Information> information)
        {
            _emitter.AddInformationToStock(information);
        }

        public override void PrintStatus(ILogger logger)
        {
            _receiver.PrintStatus(logger);
        }

        public List<Information> GetVisitors()
        {
            return _receiver.GetVisitors();
        }

        public override void Reset()
        {
            _receiver.Reset();
            _emitter.Reset();
        }

        public bool DoIHaveConnectionWith(IReceiver receiver)
        {
            return DoIHaveConnectionWith(receiver, _receiver.GetVisitors());
        }

        public bool DoIHaveConnectionWith(IReceiver receiver, List<Information> information)
        {
            return _emitter.DoIHaveConnectionWith(receiver, information);
        }

        #region Helpers

        private void Propagate()
        {
            var visitBy = _receiver.GetVisitors();
            var posiblyTargets = new List<ITransceiver>();

            foreach (var info in visitBy)
            {
                posiblyTargets.AddRange(info.GetAdjacences(_receiver));
            }

            posiblyTargets = posiblyTargets.Distinct().ToList();

            foreach (var target in posiblyTargets)
            {
                try
                {
                    TrySend(target);
                }
                catch (TransmissionException<EmitterException>)
                {
                    continue;
                }
            }
        }

        public void DecreaseStock()
        {
            _emitter.DecreaseStock();
        }

        #endregion Helpers
    }
}