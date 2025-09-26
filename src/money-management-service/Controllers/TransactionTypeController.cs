using Microsoft.AspNetCore.Mvc;
using money_management_service.Core;
using money_management_service.Entities.Transaction;
using money_management_service.Services.Interfaces;

namespace money_management_service.Controllers
{
    public class TransactionTypeController : BaseController
    {
        private readonly ITransactionTypeService _service;

        public TransactionTypeController(ITransactionTypeService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<PagedApiResponse<List<TransactionType>>>> GetTransactionType()
        {

            return Ok();
        }
    }
}
