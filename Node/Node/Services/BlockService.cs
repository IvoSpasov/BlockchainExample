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
        private List<Block> blocks;
        private ITransactionService transactionService;

        public BlockService(ITransactionService transactionService)
        {
            this.blocks = new List<Block>();
            this.transactionService = transactionService;
        }

        public Block GetLastBlock()
        {
            return this.blocks.Last();
        }

        public BlockCandidate CreateNextBlockCanidate(string minerAddress)
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
