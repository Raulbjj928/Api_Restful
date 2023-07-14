using Api_Restful.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

            var connectionString = _configuration.GetConnectionString("MyConnection");

            services.AddDbContext<ApiContext>(options => {
                options.UseSqlServer(connectionString);
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _configuration.GetSection("Jwt:Issuer").Value,
                    ValidAudience = _configuration.GetSection("Jwt:Audience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value))
                };
            });

            services.AddSingleton(_configuration);
        }
        public void Configure(IApplicationBuilder app)
        {
            // Configura o pipeline de execução do aplicativo
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            // Configura as rotas
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // Adicione esta linha se estiver usando controladores
            });

            app.UseAuthentication();
        }
    }
}
