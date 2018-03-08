namespace Node.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Node.Interfaces;
    using Node.Models;

    [Produces("application/json")]
    [Route("api/Mining")]
    public class MiningController : Controller
    {
        private INodeService nodeService;

        public MiningController(INodeService nodeService)
        {
            this.nodeService = nodeService;
        }

        [HttpGet("get-mining-job/{minerAddress}")]
        public JsonResult GetNextMiningJob(string minerAddress)
        {
            BlockCandidate bc = this.nodeService.ProcessNextBlockCandiate(minerAddress);
            var miningJob = new MiningJobResponseModel()
            {
                BlockIndex = bc.Index,
                TransactionsIncluded = bc.Transactions.Count,
                ExpectedReward = 5000350, //TODO: where is this comming from
                RewardAddress = minerAddress,
                BlockDataHash = bc.BlockDataHash,
                Difficulty = bc.Difficulty
            };

            return Json(miningJob);
        }
    }
}