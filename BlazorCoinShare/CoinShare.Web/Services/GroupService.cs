using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CoinShare.Api.Shared.Dtos;
using CoinShare.Web.Dtos;
using Microsoft.AspNetCore.Components;

namespace CoinShare.Web.Services
{
    public class GroupService
    {
        private readonly string apiBaseUrl = "https://localhost:5001/api/group";
        private readonly HttpClient httpClient;

        public GroupService(HttpClient httpClient)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<GroupDto> GetByIdAsync(Guid id)
        {
            if (id != Guid.Empty)
            {
                return await this.httpClient.GetJsonAsync<GroupDto>($"{apiBaseUrl}/{id}");
            }
            else
            {
                return new GroupDto()
                {
                    Id = Guid.Empty,
                    Persons = new List<PersonDto>()
                    {
                        new PersonDto(),
                        new PersonDto(),
                        new PersonDto()
                    }
                };
            }
        }

        public async Task<Guid> SaveAsync(GroupDto group)
        {
            if (group.Id == Guid.Empty)
            {
                return await this.httpClient.PostJsonAsync<Guid>(apiBaseUrl, group);
            }
            else
            {
                await this.httpClient.PutJsonAsync(apiBaseUrl, group);
                return (Guid)group.Id;
            }
        }
    }
}
