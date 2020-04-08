using System;
using System.Threading.Tasks;
using CoinShare.Core.Models;

namespace CoinShare.Core.Logic
{
    public interface IGroupLogic
    {
        Task<Group> GetGroupByIdAsync(Guid id);

        Task SaveGroupAsync(Group group);
    }
}
