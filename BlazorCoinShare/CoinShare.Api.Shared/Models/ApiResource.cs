using System;

namespace CoinShare.Api.Shared.Models
{
    public abstract class ApiResource<TData>
    {
        private ApiResource() { }

        public sealed class Empty : ApiResource<TData>
        {
            public Empty() : base() { }
        }

        public sealed class Loading : ApiResource<TData>
        {
            public Loading() : base() { }
        }

        public sealed class Data : ApiResource<TData>
        {
            public readonly TData Item;
            public Data(TData item) { this.Item = item; }
        }

        public sealed class Error : ApiResource<TData>
        {
            public readonly string Message;
            public readonly Exception Exception;
            public Error(string message, Exception exception) { this.Message = message; this.Exception = exception; }
        }
    }
}
