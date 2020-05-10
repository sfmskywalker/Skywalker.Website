using System.Threading.Tasks;
using Pulumi;

namespace Skywalker.Website.Infra
{
    internal static class Program
    {
        static Task<int> Main() => Deployment.RunAsync<MyStack>();
    }
}
