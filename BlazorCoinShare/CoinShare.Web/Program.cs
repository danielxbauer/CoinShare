using System.Threading.Tasks;
using CoinShare.Web.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace CoinShare.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddBaseAddressHttpClient()
                .AddSingleton<GroupService>()
                .AddSingleton<TransactionService>()
                .AddSingleton<AppState>();

            builder.RootComponents.Add<App>("app");
            await builder.Build().RunAsync();
        }
    }
}
