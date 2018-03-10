namespace Node.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Node.Interfaces;

    [Produces("application/json")]
    [Route("api/Blocks")]
    public class BlocksController : Controller
    {
        private IBlockService blockService;

        public BlocksController(IBlockService blockService)
        {
            this.blockService = blockService;
        }


        [HttpGet]
        public IActionResult GetAllBlocks()
        {
            try
            {
                var blocks = this.blockService.Blocks;
                return Json(blocks);
            }
            catch (Exception ex)
            {
                return BadRequest($"Could not get blocks: {ex}");
            }
        }
    }
}