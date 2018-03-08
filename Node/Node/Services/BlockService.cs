namespace Node.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Node.Interfaces;
    using Node.Models;
    using Node.Utilities;

    public class BlockService : IBlockService
    {
        private const int miningDifficulty = 2;
        private Dictionary<string, BlockCandidate> blockCandidates;
        private List<Block> blocks;
        private ITransactionService transactionService;

        public BlockService(ITransactionService transactionService)
        {
            this.blockCandidates = new Dictionary<string, BlockCandidate>();
            this.blocks = new List<Block>();
            this.transactionService = transactionService;
        }

        public BlockCandidate ProcessNextBlockCandiate(string minerAddress)
        {
            BlockCandidate nextBlockCandidate = this.CreateNextBlockCanidate(minerAddress, miningDifficulty);
            this.blockCandidates.Remove(minerAddress);
            this.blockCandidates.Add(minerAddress, nextBlockCandidate);
            return nextBlockCandidate;
        }

        private BlockCandidate CreateNextBlockCanidate(string minerAddress, int miningDifficulty)
        {
            // TODO: add the transaction that pays the miner. Slide 32
            var nextBlockIndex = this.blocks.Count + 1;
            var newBlockCandidate = new BlockCandidate
            {
                Index = nextBlockIndex,
                Transactions = transactionService.CreateConfirmedTransactions(nextBlockIndex),
                Difficulty = miningDifficulty,
                PreviousBlockHash = this.blocks.Any() ? this.blocks.Last().BlockHash : null,
                MinedBy = minerAddress
            };

            newBlockCandidate.BlockDataHash = Crypto.CalculateSHA256ToString(newBlockCandidate.AsJson());
            return newBlockCandidate;
        }
    }
}
