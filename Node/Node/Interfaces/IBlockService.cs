namespace Node.Interfaces
{
    using Node.ApiModels;
    using Node.Models;

    public interface IBlockService
    {
        BlockCandidate ProcessNextBlockCandiate(string minerAddress);

        void ProcessNextBlock(MiningJobRequestModel miningJobRM);
    }
}
