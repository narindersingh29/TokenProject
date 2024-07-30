using AutoMapper;

namespace TokenProject
{
    public static class ServiceBootstrapper
    {
        public static IServiceCollection AddWorkerAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfiles());
            });

            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }
    }
}
