namespace Entities.Contracts
{
    public interface INode : ITransmissionComponent
    {
        bool Equals(INode node);
        bool Equals(string label);
        string FullToString(int amount);
    }
}
