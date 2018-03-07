namespace Node.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Node.Interfaces;

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
            this.nodeService.ProcessNextMiningJob(minerAddress);
            return Json("123");
        }
    }
}