namespace Node.Interfaces
{
    using System.Collections.Generic;
    using Node.ApiModels;
    using Node.Models;

    public interface IBlockService
    {
        List<Block> Blocks { get; }

        BlockCandidate ProcessNextBlockCandiate(string minerAddress);

        void ProcessNextBlock(MiningJobRequestModel miningJobRM);
    }
}
