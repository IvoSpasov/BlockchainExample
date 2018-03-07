namespace Node.Interfaces
{
    using Node.Models;

    public interface INodeService
    {
        BlockCandidate ProcessNextMiningJob(string minerAddress);
    }
}
