namespace Node.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Node.ApiModels;
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

        public void ProcessNextBlock(MiningJobRequestModel miningJobRM)
        {
            bool isFound = this.blockCandidates.TryGetValue(miningJobRM.MinerAddress, out BlockCandidate foundBlockCandidate);
            if (!isFound)
                throw new Exception("No miner job associated with this miner address");

            Block newBlock = this.CreateNewBlock(miningJobRM, foundBlockCandidate);
            this.blocks.Add(newBlock);
            this.transactionService.ClearAllAddedToBlockPendingTransactions(foundBlockCandidate.Transactions);
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

        private Block CreateNewBlock(MiningJobRequestModel miningJobRM, BlockCandidate blockCandidate)
        {
            Block newBlock = new Block()
            {
                Index = blockCandidate.Index,
                Transactions = blockCandidate.Transactions,
                Difficulty = blockCandidate.Difficulty,
                PreviousBlockHash = blockCandidate.PreviousBlockHash,
                MinedBy = blockCandidate.MinedBy,
                BlockDataHash = blockCandidate.BlockDataHash,
                Nonce = miningJobRM.Nonce,
                DateCreated = miningJobRM.DateCreated,
                BlockHash = miningJobRM.BlockHash
            };

            return newBlock;
        }
    }
}
