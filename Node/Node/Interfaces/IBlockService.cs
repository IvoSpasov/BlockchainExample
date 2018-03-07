namespace Node.Interfaces
{
    using Node.Models;

    public interface IBlockService
    {
        int GetBlocksCount();

        Block GetLastBlock();
    }
}
