using Entities.Constants;
using Entities.Contracts;
using Entities.Loggers;
using System;
using System.Collections.Generic;

namespace Entities.Entities
{
    public partial class Receiver : Node, IReceiver
    {
        public bool CanReceive()
        {
            return _holes > 0;
        }

        public int GetNumberOfHoles()
        {
            return _holes;
        }

        public void Receive(List<Information> information)
        {
            SendTarget(information);
            UpdateState(information);
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
            Information.AddRange(information);
            _visitBy.AddRange(information);

            _holes = _holes - information.Count;
        }

        public void ReturnInformation(Information information)
        {
            _holes++;
            Information.Remove(information);
            _visitBy.Remove(information);
        }

        public List<Information> GetVisitors()
        {
            return _visitBy;
        }

        public override void Reset()
        {
            Information = new List<Information>();
            _holes = _holes + _visitBy.Count;
            _visitBy.Clear();
        }

        public override void PrintStatus(ILogger logger)
        {
            var message = 
                $"({ base.ToString() }) = " +
                $"{ FullToString(_capacity - _holes) }" +
                $"{ HolesToString()}";

            logger.PrintStatus(message);
        }

        #region Helpers

        private string HolesToString()
        {
            return string.Join("", new String(TransmissionConstants.EmptyChar, _holes));
        }

        #endregion Helpers
    }
}
