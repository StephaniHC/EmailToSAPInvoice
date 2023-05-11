using EmailToSAPInvoice.Connection; 
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection; 

namespace EmailToSAPInvoice
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
         
        public void ConfigureServices(IServiceCollection services)
        { 
            services.AddSingleton(new ServiceLayerConnection(Configuration));   
        }
         
    }
}
