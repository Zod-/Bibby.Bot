using System.IO;
using System.Threading.Tasks;

namespace Bibby.CustomVision
{
    public interface ICatOrCroissant
    {
        Task<CatOrCroissantPrediction> CatOrCroissantAsync(Stream image);
        Task<CatOrCroissantPrediction> CatOrCroissantAsync(string url);
    }
}