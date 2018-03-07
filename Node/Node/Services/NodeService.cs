﻿namespace Node.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Node.Interfaces;
    using Node.Models;

    public class NodeService
    {
        private Node node;
        private IBlockService blockService;
        private ITransactionService transactionService;

        public NodeService(IBlockService blockService, ITransactionService transactionService)
        {
            this.node = new Node();
            this.blockService = blockService;
            this.transactionService = transactionService;
        }

        public BlockCandidate ProcessNextMiningJob(string minerAddress)
        {
            var nextBlockCandidate = this.CreateNextBlockCanidate();
            // Remove previous job
            this.node.MinigJobs.Remove(minerAddress);
            // Add new job
            this.node.MinigJobs.Add(minerAddress, nextBlockCandidate);
            return nextBlockCandidate;
        }

        private BlockCandidate CreateNextBlockCanidate()
        {
            var nextBlockIndex = blockService.GetBlocksCount() + 1;
            var newBlockCanidate = new BlockCandidate();
            newBlockCanidate.Index = nextBlockIndex;
            newBlockCanidate.ConfirmedTransactions = CreateConfirmedTransactions(nextBlockIndex);


            return newBlockCanidate;
        }

        private List<ConfirmedTransaction> CreateConfirmedTransactions(int nextBlockInex)
        {
            List<Transaction> pendingTransactions = transactionService.PendingTransactions;
            var candidatesForConfirmedTransactions = pendingTransactions
                .Select(pt => new ConfirmedTransaction(pt)
                {
                    MinedInBlockIndex = nextBlockInex,
                    TransferSuccessful = true
                })
                .ToList();

            return candidatesForConfirmedTransactions;
        }
    }
}
