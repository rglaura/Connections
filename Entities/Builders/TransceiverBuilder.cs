using Entities.Entities;
using System.Collections.Generic;

namespace Entities.Builders
{
    public class TransceiverBuilder
    {
        private readonly Transceiver _transceiver;

        public TransceiverBuilder(string label, int capacity, int holes)
        {
            var receiver = new Receiver(label, capacity, holes);
            var emitter = new Emitter(label, new List<Information>());

            _transceiver = new Transceiver(receiver, emitter);
        }

        public Transceiver Get()
        {
            return _transceiver;
        }
    }
}
