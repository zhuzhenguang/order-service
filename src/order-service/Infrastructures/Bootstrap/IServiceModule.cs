using Microsoft.Extensions.DependencyInjection;

namespace order_service.Infrastructures.Bootstrap
{
    public interface IServiceModule
    {
        void Load(IServiceCollection services);
    }
}