using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CIS4930GCP.Startup))]
namespace CIS4930GCP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
