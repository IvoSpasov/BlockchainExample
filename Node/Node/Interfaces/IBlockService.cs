namespace Node.Interfaces
{
    using System.Collections.Generic;
    using Node.ApiModels;
    using Node.Models;

    public interface IBlockService
    {
        List<Block> Blocks { get; }

        Block GetBlock(int index);

        BlockCandidate ProcessNextBlockCandiate(string minerAddress);

        void ProcessNextBlock(MiningJobRequestModel miningJobRM);

        ConfirmedTransaction GetConfrimedTransaction(string tranHash);

        List<ConfirmedTransaction> GetAllConfirmedTransactions();

        IEnumerable<ConfirmedTransaction> GetConfirmedTransactions(string address);

        IEnumerable<ConfirmedTransaction> GetConfrimedTransactions(string address, int blockConfirmationsCount);
    }
}
