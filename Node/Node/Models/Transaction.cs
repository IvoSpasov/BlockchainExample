namespace Node.Models
{
    using System;

    public class Transaction
    {
        public string From { get; set; }

        public string To { get; set; }

        public string SenderPublicKey { get; set; }

        public int Value { get; set; }

        public int Fee { get; set; }

        public DateTime DateCreated { get; set; }

        public string[] SenderSignature { get; set; }

        public string Hash { get; set; }

        public int? MinedInBlockIndex { get; set; }

        public bool TransferSuccessful { get; set; }
        
        public string AsJsonString(bool withSignature)
        {
            string tran = $@"{{""from"":""{From}"",""to"":""{To}"",""senderPubKey"":""{SenderPublicKey}"",
                        ""value"":""{Value}"",""fee"":""{Fee}"",""dateCreated"":""{DateCreated.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")}""";

            if (withSignature)
            {
                string signature = $@",""senderSignature"":[""{SenderSignature[0]}"",""{SenderSignature[1]}""]}}";
                return tran + signature;
            }

            return tran + "}";
        }
    }
}
