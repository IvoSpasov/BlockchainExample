namespace Node.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Node.Interfaces;
    using Node.Models;

    public class BlockService : IBlockService
    {
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

        public BlockCandidate CreateNextBlockCanidate()
        {
            var nextBlockIndex = this.blocks.Count + 1;
            var newBlockCanidate = new BlockCandidate();
            newBlockCanidate.Index = nextBlockIndex;
            newBlockCanidate.ConfirmedTransactions = transactionService.CreateConfirmedTransactions(nextBlockIndex);


            return newBlockCanidate;
        }
    }
}
