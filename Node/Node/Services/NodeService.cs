namespace Node.Services
{
    using Node.Interfaces;
    using Node.Models;

    public class NodeService : INodeService
    {
        private const int miningDifficulty = 2;
        private Node node;

        public NodeService()
        {
            this.node = new Node
            {
                Difficulty = miningDifficulty
            };
        }
    }
}
