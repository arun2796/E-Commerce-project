using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Microsoft.Extensions.DependencyInjection;


namespace Application.RegisterAppServices
{
    public static class ApplicationRegister
    {
        public static IServiceCollection ApplicationService(this IServiceCollection services)
        {
            // Fix: Use the correct overload of AddAutoMapper that accepts a configuration action
            services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());
            return services;
        }
    }
}
