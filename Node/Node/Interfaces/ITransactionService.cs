namespace Node.Interfaces
{
    using Node.ViewModels;

    public interface ITransactionService
    {
        void Process(TransactionVM tranVM);
    }
}
