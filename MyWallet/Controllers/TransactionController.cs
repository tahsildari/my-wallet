using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWallet.Api.Constants;
using MyWallet.Models;
using MyWallet.Service;
using MyWallet.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWallet.Controllers
{
    [ApiController]
    [Route("Wallet/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly IOperationsService operationsService;

        public TransactionController(ILogger<TransactionController> logger, IOperationsService operationsService)
        {
            this.operationsService = operationsService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create(TransactionModel transaction)
        {
            TransactionValidation validator = new TransactionValidation();
            var validationResult = validator.Validate(transaction);

            if (!validationResult.IsValid)
            {
                return ValidationProblem(validationResult.ReadValidationErrors());
            }
            try
            {
                var success = await this.operationsService.AddTransaction(transaction);
                if (success)
                    return Ok();
                else 
                    return Problem (detail: ErrorMessages.SomethingWentWrong);

            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, "");
                return Problem(ex.Message);
            }
        }
    }
}
