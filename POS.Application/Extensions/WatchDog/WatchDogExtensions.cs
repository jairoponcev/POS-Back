using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WatchDog;
using WatchDog.src.Enums;

namespace POS.Application.Extensions.WatchDog
{
    public static class WatchDogExtensions
    {
        public static IServiceCollection AddWatchDog(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddWatchDogServices(options =>
            {
                options.IsAutoClear = true;
                options.ClearTimeSchedule = WatchDogAutoClearScheduleEnum.Daily;
                options.SetExternalDbConnString = configuration.GetConnectionString("PosConnection");
                options.DbDriverOption = WatchDogDbDriverEnum.MSSQL;
            });

            return services;
        }
    }
}
