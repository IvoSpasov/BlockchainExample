namespace Node.ApiModels
{
    public class BalanceForAddressResponseModel
    {
        public BalanceForAddressResponseModel(
            string address,
            bool anyPendingTran, long pendingBalance,
            bool anyLastMinedTran, long lastMinedBalance,
            bool anyConfirmedTran, long confirmedBalance)
        {
            this.Address = address;
            if (anyPendingTran)
                this.PendingBalance = new ConfirmationsAndBalance() { Balance = pendingBalance };

            if (anyLastMinedTran)
                this.LastMinedBalance = new ConfirmationsAndBalance() { Confirmations = 0, Balance = lastMinedBalance };

            if (anyConfirmedTran)
                this.ConfirmedBalance = new ConfirmationsAndBalance() { Confirmations = 6, Balance = confirmedBalance };
            // TODO: 6 must be added to a global constant or parameter. There is another one used in the address service.
        }

        public string Address { get; set; }

        public ConfirmationsAndBalance ConfirmedBalance { get; set; }

        public ConfirmationsAndBalance LastMinedBalance { get; set; }

        public ConfirmationsAndBalance PendingBalance { get; set; }
    }

    public class ConfirmationsAndBalance
    {
        // if the transactions are not yet in a block, then we cannot say there are confrimations, right?
        // that's why I used nullable int
        public int? Confirmations { get; set; }

        public long Balance { get; set; }
    }
}
