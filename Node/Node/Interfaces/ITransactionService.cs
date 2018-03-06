namespace Node.Interfaces
{
    using Node.Models;
    using Node.ViewModels;

    public interface ITransactionService
    {
        Transaction GetTransaction(string tranHash);

        void Process(TransactionVM tranVM);
    }
}
