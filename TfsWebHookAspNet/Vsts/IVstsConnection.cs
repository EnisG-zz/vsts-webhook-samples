using System.Net.Http;

namespace TfsWebHookAspNet.Vsts
{
    public interface IVstsConnection
    {
        HttpClient Client { get;  }
        string CollectionUrl { get; }
    }
}