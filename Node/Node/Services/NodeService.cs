namespace Node.Services
{
    using System.Collections.Generic;
    using Node.Interfaces;
    using Node.Models;

    public class NodeService : INodeService
    {
        private const int miningDifficulty = 2;
        private IBlockService blockService;
        private Node node;

        public NodeService(IBlockService blockService)
        {
            this.node = new Node
            {
                BlockCandidates = new Dictionary<string, BlockCandidate>(),
                Difficulty = miningDifficulty
            };

            this.blockService = blockService;
        }

        public BlockCandidate ProcessNextBlockCandiate(string minerAddress)
        {
            BlockCandidate nextBlockCandidate = this.blockService.CreateNextBlockCanidate(minerAddress, miningDifficulty);
            this.node.BlockCandidates.Remove(minerAddress);
            this.node.BlockCandidates.Add(minerAddress, nextBlockCandidate);
            return nextBlockCandidate;
        }
    }
}
