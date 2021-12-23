using System.Collections;
using System.Threading.Tasks;

namespace Question4.Services
{
    public interface INewsService
    {
        Task<IEnumerable> GetNewsEventsAsync();
    }
}
