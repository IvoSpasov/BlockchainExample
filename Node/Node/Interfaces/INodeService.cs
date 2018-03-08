namespace Node.Interfaces
{
    using Node.Models;

    public interface INodeService
    {
        BlockCandidate ProcessNextBlockCandiate(string minerAddress);
    }
}
