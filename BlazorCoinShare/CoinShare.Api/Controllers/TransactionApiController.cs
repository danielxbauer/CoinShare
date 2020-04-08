using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoinShare.Api.Shared.Dtos;
using CoinShare.Core.Logic;
using CoinShare.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoinShare.Api.Controllers
{
    [ApiController]
    [Route("api/transaction")]
    public class TransactionApiController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ITransactionLogic transactionLogic;

        public TransactionApiController(
            IMapper mapper,
            ITransactionLogic transactionLogic)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.transactionLogic = transactionLogic ?? throw new ArgumentNullException(nameof(transactionLogic));
        }

        [HttpGet]
        [Route("group/{groupId:guid}/persons")]
        public async Task<ActionResult<IEnumerable<PersonOverviewDto>>> GetPersonOverviewsAsync(Guid groupId)
        {
            IEnumerable<PersonOverview> personOverviews = await this.transactionLogic.GetPersonOverviewsAsync(groupId);
            return Ok(this.mapper.Map<IEnumerable<PersonOverviewDto>>(personOverviews));
        }

        [HttpGet]
        [Route("group/{groupId:guid}")]
        public async Task<ActionResult<PaginationDto<TransactionDto>>> 
            GetByGroupId(Guid groupId, [FromQuery] int offset, [FromQuery] int take)
        {
            var allTransactions = await this.transactionLogic.GetTransactionsByGroupIdAsync(groupId);
            var transactions = allTransactions
                .OrderByDescending(t => t.PaidOn)
                .Skip(offset)
                .Take(take);

            return Ok(new PaginationDto<TransactionDto>
            {
                Items = this.mapper.Map<IList<TransactionDto>>(transactions),
                Offset = offset + transactions.Count(),
                Total = allTransactions.Count()
            });
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] TransactionDto transactionDto)
        {
            Transaction transaction = this.mapper.Map<Transaction>(transactionDto);
            await this.transactionLogic.SaveTransactionAsync(transaction);
            return Ok(transaction.Id);
        }
    }
}
