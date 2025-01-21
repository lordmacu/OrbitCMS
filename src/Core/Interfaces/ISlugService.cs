using System.Threading.Tasks;
using Core.Interfaces;

namespace Application.Common
{
    public interface ISlugService
    {
        Task<string> GenerateUniqueSlugAsync(string title, ISlugRepository repository);
    }
}
