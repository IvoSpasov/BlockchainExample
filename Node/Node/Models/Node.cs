namespace Node.Models
{
    using System.Collections.Generic;

    public class Node
    {
        public int Difficulty { get; set; }

        public List<string> Peers { get; set; }
    }
}
