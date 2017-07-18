using Entities.Constants;
using Entities.Contracts;
using Entities.Exceptions;
using Entities.Loggers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Entities
{
    public partial class Emitter : Node, IEmitter
    {
        public void SetReachables(List<IReceiver> reachables)
        {
            _reachables = reachables;
        }

        public void TrySend(IReceiver target)
        {
            if (!DoIHaveInformation())
            {
                throw new TransmissionException<EmitterException>("I have no information to send.");
            }

            if (!CanIReach(target))
            {
                throw new TransmissionException<EmitterException>($"I can not reach the target {target.ToString()}.");
            }

            if (!MayIEmitTo(target))
            {
                throw new TransmissionException<EmitterException>($"The target {target.ToString()} can not receive my information.");
            }

            var holes = target.GetNumberOfHoles();
            var amount = Math.Min(_stock, holes);
            var informationToSent = SelectInformationToSend(amount);

            UpdateStock(amount);
            target.Receive(informationToSent);
        }

        public void AddInformationToStock(List<Information> information)
        {
            Information.AddRange(information);
            _stock = Information.Count;
        }

        public void DecreaseStock()
        {
            _stock--;
        }

        public bool DoIHaveConnectionWith(IReceiver receiver)
        {
            return DoIHaveConnectionWith(receiver, _initialInformations);
        }

        public bool DoIHaveConnectionWith(IReceiver receiver, List<Information> information)
        {
            foreach (var info in information)
            {
                if (info.Links(this, receiver))
                {
                    return true;
                }
            }

            return false;
        }

        public void ReclaimInformation(Information information)
        {
            AddInformationToStock(new List<Information> { information });
        }

        public override void Reset()
        {
            Information = _initialInformations.ToList();
            Initiate();
        }

        public override void PrintStatus(ILogger logger)
        {
            var message = 
                $"({ base.ToString() }) = " + 
                $"{ FullToString(Information.Count()) }";

            logger.PrintStatus(message);
        }

        #region Helpers

        private void Initiate()
        {
            foreach (var information in _initialInformations)
            {
                information.AddInitialNode(this);
            }

            _stock = _initialInformations.Count;
        }

        private bool DoIHaveInformation()
        {
            return _stock > 0;
        }

        private bool CanIReach(IReceiver target)
        {
            return _reachables.Any(r => r.ToString().ToLower() == target.ToString().ToLower());
        }

        private bool MayIEmitTo(IReceiver target)
        {
            return target.CanReceive();
        }

        private List<Information> SelectInformationToSend(int amount)
        {
            return Information.Take(amount).ToList();
        }

        private void UpdateStock(int amount)
        {
            Information.RemoveRange(0, amount);
            _stock -= amount;
        }

        #endregion Helpers
    }
}
