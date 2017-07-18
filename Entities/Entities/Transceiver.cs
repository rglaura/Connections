using Entities.Contracts;
using System.Collections.Generic;

namespace Entities.Entities
{
    public partial class Transceiver
    {
        private IReceiver _receiver;
        private IEmitter _emitter;

        public Transceiver(IReceiver receiver, IEmitter emitter)
            : base(receiver.ToString(), new List<Information>())
        {
            _receiver = receiver;
            _emitter = emitter;
        }
    }
} 