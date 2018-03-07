namespace Node.Interfaces
{
    using Node.Models;

    public interface IBlockService
    {
        Block GetLastBlock();

        BlockCandidate CreateNextBlockCanidate(string minerAddress);
    }
}
