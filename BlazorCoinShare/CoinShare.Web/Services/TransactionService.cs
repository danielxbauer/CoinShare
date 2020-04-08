using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CoinShare.Api.Shared.Dtos;
using Microsoft.AspNetCore.Components;

namespace CoinShare.Web.Services
{
    public class TransactionService
    {
        private readonly string apiBaseUrl = "https://localhost:5001/api/transaction";
        private readonly HttpClient httpClient;

        public TransactionService(HttpClient httpClient)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public Task<PaginationDto<TransactionDto>> GetByGroupId(Guid groupId, int offset, int take)
        {
            return this.httpClient
                .GetJsonAsync<PaginationDto<TransactionDto>>($"{apiBaseUrl}/group/{groupId}?offset={offset}&take={take}");
        }

        public Task<IList<PersonOverviewDto>> GetPersonOverviewsAsync(Guid groupId)
        {
            return this.httpClient.GetJsonAsync<IList<PersonOverviewDto>>($"{apiBaseUrl}/group/{groupId}/persons");
        }

        public Task<int> CreateAsync(TransactionDto transaction)
        {
            return this.httpClient.PostJsonAsync<int>(apiBaseUrl, transaction);
        }
    }
}
