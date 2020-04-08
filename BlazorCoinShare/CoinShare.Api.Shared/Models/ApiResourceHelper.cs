using System;
using System.Threading.Tasks;

namespace CoinShare.Api.Shared.Models
{
    public static class ApiResourceHelper
    {
        public static async Task GetResource<TData>(
            Func<Task<TData>> from,
            Action<ApiResource<TData>> onChange)
        {
            onChange(Loading<TData>());

            try
            {
                var data = await from();
                onChange(Data<TData>(data));
            }
            catch (Exception ex)
            {
                onChange(Error<TData>("error while loading", ex));
            }
        }

        public static ApiResource<TData>.Empty Empty<TData>()
            => new ApiResource<TData>.Empty();

        public static ApiResource<TData>.Loading Loading<TData>()
            => new ApiResource<TData>.Loading();

        public static ApiResource<TData>.Data Data<TData>(TData data)
            => new ApiResource<TData>.Data(data);

        public static ApiResource<TData>.Error Error<TData>(string message, Exception ex)
            => new ApiResource<TData>.Error(message, ex);

        public static bool IsEmpty<TData>(ApiResource<TData> resource)
            => resource is ApiResource<TData>.Empty;

        public static bool IsData<TData>(ApiResource<TData> resource)
            => resource is ApiResource<TData>.Data;

        public static T IfDataGet<T, TData>(this ApiResource<TData> resource, Func<TData, T> selector, T @default)
        {
            return resource is ApiResource<TData>.Data data
                ? selector(data.Item)
                : @default;
        }
    }
}
