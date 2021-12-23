using Question4.Data;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Question4.Services
{
    public interface INewsService
    {
        Task<IEnumerable<NewsItem>> GetNewsEventsAsync();
    }
}
