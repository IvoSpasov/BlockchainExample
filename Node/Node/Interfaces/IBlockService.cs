namespace Node.Interfaces
{
    using Node.Models;

    public interface IBlockService
    {
        BlockCandidate ProcessNextBlockCandiate(string minerAddress);
    }
}
