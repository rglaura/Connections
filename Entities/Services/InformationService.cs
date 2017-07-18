using Entities.Contracts;
using Entities.Loggers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Entities
{
    public partial class Information
    {
        public void AddInitialNode(IEmitter emitter)
        {
            _status.Add(emitter);
        }

        public void AddTarget(IReceiver receiver)
        {
            _status.Add(receiver);
        }

        public void TryRemoveConnection(IEmitter emitter, IReceiver receiver)
        {
            var connectionIndex = ConnectionIndex(emitter, receiver);

            if (connectionIndex > -1)
            {
                RemoveTargetAt(connectionIndex);
            }
        }

        public List<ITransceiver> GetAdjacences(IReceiver receiver)
        {
            var adjacences = new List<ITransceiver>();

            for (int i = 0; i < _status.Count - 1; i++)
            {
                if (_status[i].Equals(receiver))
                {
                    adjacences.Add((ITransceiver)_status[i + 1]);
                }
            }

            return adjacences;
        }

        public bool Links(IEmitter emitter, IReceiver receiver)
        {
            return ConnectionIndex(emitter, receiver) > -1;
        }

        public void Reset()
        {
            _status.Clear();
        }

        public override string ToString()
        {
            return _label;
        }

        public void PrintStatus(ILogger logger)
        {
            logger.PrintStatus(
                $"{ToString()} = [{ string.Join(", ", _status.Select(node => node.ToString()))}]"
                );
        }

        #region helpers

        private int ConnectionIndex(IEmitter origin, IReceiver target)
        {
            for (int i = 0; i < _status.Count - 1; i++)
            {
                if (_status[i].Equals(origin) && _status[i + 1].Equals(target))
                {
                    return i;
                }
            }

            return -1;
        }

        private void RemoveTargetAt(int index)
        {
            for (int i = _status.Count - 1; i >= index + 1; i--)
            {
                var askedFor = _status[i] as ITransceiver ?? _status[i] as IReceiver;
                askedFor.ReturnInformation(this);

                var reclaimer = _status[i - 1] as ITransceiver ?? _status[i - 1] as IEmitter;
                reclaimer.ReclaimInformation(this);
            }
            /* Falta tener en cuenta visitBy_ */
            _status.RemoveRange(index + 1, _status.Count - (index + 1));
        }

        #endregion helpers
    }
}