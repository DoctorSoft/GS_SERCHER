using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GomelSat.Startup))]
namespace GomelSat
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
