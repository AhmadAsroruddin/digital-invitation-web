using Microsoft.Extensions.DependencyInjection;
using WebApi.Application.Interfaces.Repository;
using WebApi.Application.Interfaces.Service;
using WebApi.Infrastructure.Repository;
using WebApi.Infrastructure.Services;

namespace WebApi.Infrastructure.DependencyInjection
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<JwtTokenGenerator>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IContactService, ContactService>();

            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IEventService, EventService>();

            services.AddScoped<ISubEventRepository, SubEventRepository>();
            services.AddScoped<ISubEventService, SubEventService>();

            services.AddScoped<IGuestRepository, GuestRepository>();
            services.AddScoped<IGuestService, GuestService>();

            services.AddScoped<IGuestSubEventRepository, GuestSubEventRepository>();
            services.AddScoped<IGuestSubEventService, GuestSubEventService>();

            services.AddScoped<IRSPVRepository, RSVPRepository>();
            services.AddScoped<IRSVPService, RSVPService>();

            services.AddScoped<ICheckInRepository, CheckInRepository>();
            services.AddScoped<ICheckInService, CheckInService>();
            
            return services;
        }
    }
}