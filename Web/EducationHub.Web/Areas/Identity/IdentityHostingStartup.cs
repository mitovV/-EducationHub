using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(EducationHub.Web.Areas.Identity.IdentityHostingStartup))]

namespace EducationHub.Web.Areas.Identity
{

    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}