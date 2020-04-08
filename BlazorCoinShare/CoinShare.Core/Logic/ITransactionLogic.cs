using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoinShare.Core.Models;

namespace CoinShare.Core.Logic
{
    public interface ITransactionLogic
    {
        Task<IEnumerable<Transaction>> GetTransactionsByGroupIdAsync(Guid groupId);
        Task<Transaction> GetTransactionByIdAsync(int id);
        Task SaveTransactionAsync(Transaction transaction);
        Task<IEnumerable<PersonOverview>> GetPersonOverviewsAsync(Guid groupId);
    }
}
