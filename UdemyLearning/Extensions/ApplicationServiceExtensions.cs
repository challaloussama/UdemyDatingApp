using Microsoft.EntityFrameworkCore;
using UdemyLearning.Data;
using UdemyLearning.Interfaces;
using UdemyLearning.Services;

namespace UdemyLearning.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection addApplicationServices(this IServiceCollection services,IConfiguration config)
        {
            services.AddScoped<ITokenService, TokenService>();

            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}
