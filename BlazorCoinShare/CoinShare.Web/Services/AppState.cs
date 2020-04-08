using System;
using System.Threading.Tasks;
using CoinShare.Api.Shared.Models;
using CoinShare.Web.Dtos;
using static CoinShare.Api.Shared.Models.ApiResourceHelper;

namespace CoinShare.Web.Services
{
    public class AppState
    {
        private readonly GroupService groupService;
        private ApiResource<GroupDto> group = Empty<GroupDto>();

        public AppState(
            GroupService groupService)
        {
            this.groupService = groupService ?? throw new ArgumentNullException(nameof(groupService));
        }

        public event Action<ApiResource<GroupDto>> OnGroupChange;

        public async Task GetGroupById(Guid id)
        {
            if (this.group.IfDataGet(d => d.Id, Guid.Empty) == id)
            {
                OnGroupChange?.Invoke(this.group);
                return;
            }

            await GetResource(
                from: () => this.groupService.GetByIdAsync(id),
                onChange: g =>
                {
                    this.group = g;
                    this.OnGroupChange?.Invoke(g);
                });
        }
    }
}
