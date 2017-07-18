using Application.Exceptions;
using Application.Extensions;
using Entities.Contracts;
using Entities.Entities;
using Entities.Exceptions;
using Entities.Loggers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Application
{
    public class Level
    {
        private List<Information> _information;
        private List<INode> _nodes;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ILogger _logger;

        public Level(List<Information> information, List<INode> nodes, ILogger logger)
        {
            _information = information;
            _nodes = nodes;

            _logger = logger;
        }

        public void Connect(IEmitter origin, IReceiver target)
        {
            if (origin.DoIHaveConnectionWith(target))
            {
                throw new TransmissionException<LevelException>($"{origin} is already connected with {target}");
            }

            origin.TrySend(target);
        }

        public void RemoveConnection(IEmitter origin, IReceiver target)
        {
            if (!origin.DoIHaveConnectionWith(target))
            {
                throw new TransmissionException<LevelException>($"There is no any connection between {origin} and {target}");
            }

            foreach (var information in _information)
            {
                information.TryRemoveConnection(origin, target);
            }
        }

        public void PrintStatus()
        {
            _logger.PrintSeparator('-');
            _information.PrintComponents(_logger);
            _logger.PrintSeparator('.');
            _nodes.PrintComponents(_logger);
            _logger.PrintSeparator('-');
        }

        public T TryGet<T>(string label)
        {
            var node = _nodes.FirstOrDefault(n => n.Equals(label));

            if (node == null)
            {
                throw new TransmissionException<LevelException>($"Node {label} does not belong to this level");
            }

            try
            {
                return (T)node;
            }
            catch
            {
                throw new TransmissionException<LevelException>($"{label} is not of type {typeof(T).Name}");
            }
        }

        public void Reset()
        {
            _information.Reset();
            _nodes.Reset();
        }
    }
}