using System.Threading.Tasks;
using Itbeard.Models;

namespace Itbeard.Services.Interfaces
{
    public interface IUrlService
    {
        Task<UrlModel> Reduce(string targetUrl);
        Task<UrlModel> Get(string shortGuid);
    }
}