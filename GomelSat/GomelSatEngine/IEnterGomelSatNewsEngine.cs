using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace GomelSatEngine
{
    public interface IEnterGomelSatNewsEngine
    {
        void Run(string header, string shortText, string text);
    }
}
