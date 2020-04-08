using System.Collections.Generic;

namespace CoinShare.Api.Shared.Dtos
{
    public class PaginationDto<T>
    {
        public IList<T> Items { get; set; }
        public int Offset { get; set; }
        public int Total { get; set; }
    }
}
