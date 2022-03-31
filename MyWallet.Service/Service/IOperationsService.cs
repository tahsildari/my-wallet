using MyWallet.Models;
using System.Threading.Tasks;

namespace MyWallet.Service
{
    public interface IOperationsService
    {
        Task<bool> AddTransaction(TransactionModel transaction);
    }
}