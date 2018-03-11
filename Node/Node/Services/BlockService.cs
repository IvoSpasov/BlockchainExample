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
        private const int blockMiningDifficulty = 2;
        private Dictionary<string, BlockCandidate> blockCandidates;
        private List<Block> blocks;
        private ITransactionService transactionService;

        public BlockService(ITransactionService transactionService)
        {
            this.blockCandidates = new Dictionary<string, BlockCandidate>();
            this.blocks = new List<Block>();
            this.transactionService = transactionService;
        }

        public List<Block> Blocks
        {
            get
            {
                return this.blocks;
            }
        }

        public Block GetBlock(int index)
        {
            return this.blocks.First(b => b.Index == index);
        }

        public BlockCandidate ProcessNextBlockCandiate(string minerAddress)
        {
            BlockCandidate nextBlockCandidate = this.CreateNextBlockCanidate(minerAddress, blockMiningDifficulty);
            this.blockCandidates.Remove(minerAddress);
            this.blockCandidates.Add(minerAddress, nextBlockCandidate);
            return nextBlockCandidate;
        }

        public void ProcessNextBlock(MiningJobRequestModel miningJobRM)
        {
            bool isFound = this.blockCandidates.TryGetValue(miningJobRM.MinerAddress, out BlockCandidate foundBlockCandidate);
            if (!isFound)
                throw new Exception("No miner job associated with this miner address.");

            if (foundBlockCandidate.BlockDataHash != miningJobRM.BlockDataHash)
                throw new Exception("Mining job block data hash is different than the hash of the prepared block candidate.");

            if (this.blocks.Any() && foundBlockCandidate.Index <= this.blocks.Last().Index)
            {
                this.SyncNode(foundBlockCandidate.Index);
                throw new Exception($"Sorry but a block with index {foundBlockCandidate.Index} was already mined.");
            }

            if (!this.IsFoundBlockHashValid(miningJobRM))
                throw new Exception("Found block hash is invalid");
            //TODO: During synchronization with other nodes, a check for valid hash for incoming blocks must be done as well.

            Block newBlock = this.CreateNewBlock(miningJobRM, foundBlockCandidate);
            this.blocks.Add(newBlock);
            this.transactionService.ClearAllAddedToBlockPendingTransactions(newBlock.ConfirmedTransactions);
            this.blockCandidates.Clear();
        }

        public ConfirmedTransaction GetConfrimedTransaction(string tranHash)
        {
            foreach (var block in this.Blocks)
            {
                if (block.ConfirmedTransactions.Any(t => t.Hash == tranHash))
                {
                    return block.ConfirmedTransactions.First(t => t.Hash == tranHash);
                }
            }

            return null;
        }

        public List<ConfirmedTransaction> GetAllConfirmedTransactions()
        {
             return this.Blocks.SelectMany(b => b.ConfirmedTransactions).ToList();
        }

        public IEnumerable<ConfirmedTransaction> GetConfirmedTransactions(string address)
        {
            return this.Blocks.SelectMany(b => b.ConfirmedTransactions.Where(t => t.From == address || t.To == address));
        }

        public IEnumerable<ConfirmedTransaction> GetConfrimedTransactions(string address, int blockConfirmationsCount)
        {
            IEnumerable<ConfirmedTransaction> secureTransactions = new List<ConfirmedTransaction>();
            if (!this.blocks.Any())
                return secureTransactions;

            var indexOfLastBlock = this.blocks.Last().Index;
            if (indexOfLastBlock <= blockConfirmationsCount)
                return secureTransactions;

            var indexOfSecureBlock = indexOfLastBlock - blockConfirmationsCount;
            var secureBlocks = this.blocks.Where(b => b.Index <= indexOfSecureBlock);
            secureTransactions = secureBlocks.SelectMany(b => b.ConfirmedTransactions.Where(t => t.From == address || t.To == address));
            return secureTransactions;
        }

        private BlockCandidate CreateNextBlockCanidate(string minerAddress, int miningDifficulty)
        {
            // TODO: add the transaction that pays the miner. Slide 32
            var nextBlockIndex = this.blocks.Count + 1;
            var newBlockCandidate = new BlockCandidate
            {
                Index = nextBlockIndex,
                ConfirmedTransactions = transactionService.CreateConfirmedTransactions(nextBlockIndex),
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
                ConfirmedTransactions = blockCandidate.ConfirmedTransactions,
                Difficulty = blockCandidate.Difficulty,
                PreviousBlockHash = blockCandidate.PreviousBlockHash,
                MinedBy = blockCandidate.MinedBy,
                BlockDataHash = blockCandidate.BlockDataHash,
                Nonce = miningJobRM.Nonce,
                DateCreated = miningJobRM.Timestamp,
                BlockHash = miningJobRM.MinedBlockHash
            };

            return newBlock;
        }

        private void SyncNode(int foundBlockCanidateIndex)
        {
            this.blockCandidates.Clear();
            for (int i = foundBlockCanidateIndex; i <= this.blocks.Last().Index; i++)
            {
                this.transactionService.ClearAllAddedToBlockPendingTransactions(this.blocks[i].ConfirmedTransactions);
            }
        }

        private bool IsFoundBlockHashValid(MiningJobRequestModel miningJobRM)
        {
            // Can be added to utils if needed elsewhere
            string timestampIso8601 = miningJobRM.Timestamp.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            string combinedInput = $"{miningJobRM.BlockDataHash}{timestampIso8601}{miningJobRM.Nonce}";
            string calculatedHash = Crypto.CalculateSHA256ToString(combinedInput);
            return miningJobRM.MinedBlockHash == calculatedHash;
        }
    }
}
