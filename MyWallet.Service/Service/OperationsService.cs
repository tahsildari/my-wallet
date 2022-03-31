using MyWallet.Models;
using System;
using System.Threading.Tasks;

namespace MyWallet.Service
{
    public class OperationsService : IOperationsService
    {
        private readonly IDapperService<object> dapperService;

        public OperationsService(IDapperService<object> dapperService)
        {
            this.dapperService = dapperService;
        }
        public async Task<bool> AddTransaction(TransactionModel transaction)
        {
            // The reason I am implementing the logic in sql (stored procedures) is that
            // I don't want to make a lot of roundtrips to database.
            // This approach is faster and more efficient than getting balance from database in 1 request
            // and then make the transaction in another request.
            var affectedRows = await dapperService.ExecuteAsync(Constants.StoredProcedures.AddTransaction,
            System.Data.CommandType.StoredProcedure,
            new
            {
                id = transaction.Id,
                amount = transaction.Amount,
                direction = transaction.Direction,
                account = transaction.Account
            });

            return affectedRows > 0;
        }
    }
}
