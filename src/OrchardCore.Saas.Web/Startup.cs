using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrchardCore.ResourceManagement.TagHelpers;

namespace OrchardCore.Saas.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Add ASP.NET MVC and support for modules
            var builder = services.AddOrchardCore()

                .AddCommands()

                .AddSecurity()
                .AddMvc()
                .AddIdGeneration()
                .AddEmailAddressValidator()
                .AddHtmlSanitizer()
                .AddSetupFeatures("OrchardCore.Setup")

                .AddDataAccess()
                .AddDataStorage()
                .AddBackgroundService()
                .AddScripting()

                .AddTheming()
                .AddCaching();

            // OrchardCoreBuilder is not available in OrchardCore.ResourceManagement as it has to
            // remain independent from OrchardCore.
            builder.ConfigureServices(s =>
            {
                s.AddResourceManagement();

                s.AddTagHelpers<LinkTagHelper>();
                s.AddTagHelpers<MetaTagHelper>();
                s.AddTagHelpers<ResourcesTagHelper>();
                s.AddTagHelpers<ScriptTagHelper>();
                s.AddTagHelpers<StyleTagHelper>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseOrchardCore();
        }
    }
}
