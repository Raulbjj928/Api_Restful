using Api_Restful.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Api_Restful
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
            services.AddControllers();

            var connectionString = "";   // _configuration.GetConnectionString("MyConnection");

            services.AddDbContext<ApiContext>(options => {
                options.UseSqlServer(connectionString);
            });

        }
        public void Configure(IApplicationBuilder app)
        {
            // Configura o pipeline de execução do aplicativo
            app.UseRouting();

            // Configura as rotas
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // Adicione esta linha se estiver usando controladores
            });
        }
    }
}
