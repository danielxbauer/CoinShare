using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoinShare.Core.Models;

namespace CoinShare.Core.Logic
{
    public interface IPersonLogic
    {
        Task<IEnumerable<Person>> GetPersonsByGroupIdAsync(Guid groupId);
        Task SavePersonAsync(Person person);
    }
}
