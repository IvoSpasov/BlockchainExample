namespace Node.Services
{
    using System.Collections.Generic;
    using Node.Interfaces;
    using Node.Models;

    public class NodeService : INodeService
    {
        private Node node;
        private IBlockService blockService;

        public NodeService(IBlockService blockService)
        {
            this.node = new Node
            {
                BlockCandidates = new Dictionary<string, BlockCandidate>()
            };

            this.blockService = blockService;
        }

        public BlockCandidate ProcessNextBlockCandiate(string minerAddress)
        {
            BlockCandidate nextBlockCandidate = this.blockService.CreateNextBlockCanidate(minerAddress);
            this.node.BlockCandidates.Remove(minerAddress);
            this.node.BlockCandidates.Add(minerAddress, nextBlockCandidate);
            return nextBlockCandidate;
        }
    }
}
