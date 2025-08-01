using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.IRepository;
using Microsoft.Extensions.DependencyInjection;

namespace CInfrastructure.Repository
{
    public static class InfrastructureRegister
    {

        public static IServiceCollection InfractureService(this IServiceCollection services)
        {
            services.AddScoped<IBasketRepository ,BasketRepository>();
            return services;
        }
    }
}
