using Microsoft.Extensions.DependencyInjection;

namespace Project.API.Configurations
{
    /// <summary>
    /// Definições de Policies do sistema.
    /// </summary>
    public static class PoliciesConfiguration
    {
        public static void RegisterPoliciesConfiguration(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
                options.AddPolicy("RequireMemberRole", policy => policy.RequireRole("Member"));
            });
        }
    }
}
