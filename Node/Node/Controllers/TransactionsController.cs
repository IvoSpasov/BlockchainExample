using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Node.ViewModels;

namespace Node.Controllers
{
    [Produces("application/json")]
    [Route("api/Transactions")]
    public class TransactionsController : Controller
    {
        [HttpPost("send")]
        public string Send([FromBody]TransactionVM transactionVM)
        {
            return "sucessfully added a transaction";
        }
    }
}