using System.Security.Cryptography.X509Certificates;

namespace GomelSatEngine
{
    public interface IEnterGomelSatNewsEngine
    {
        void Run(string header, string shortText, string text);
    }
}
