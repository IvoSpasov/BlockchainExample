namespace Node.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Node.Interfaces;
    using Node.Models;

    public class BlockService : IBlockService
    {
        private List<Block> blocks;

        public BlockService()
        {
            this.blocks = new List<Block>();
        }

        public int GetBlocksCount()
        {
            return this.blocks.Count;
        }

        public Block GetLastBlock()
        {
            return this.blocks.Last();
        }
    }
}
